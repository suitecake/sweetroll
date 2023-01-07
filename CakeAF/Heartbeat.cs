using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Camille.Enums;
using Camille.RiotGames;
using Camille.RiotGames.MatchV5;

using CakeAF.Entity;

namespace CakeAF
{
    public class Heartbeat
    {
        private TeamRecordService teamRecordService = null;

        private readonly DbCache cache = new DbCache();
        private const int MinimumParticipantCountForQuorum = 4;

        [FunctionName("Heartbeat")]
        public async Task Run([TimerTrigger("0 */3 * * * *")] TimerInfo myTimer, ILogger log)
        {
            using (var dbContext = new CakeDBContext())
            {
                await ProcessMatches(dbContext);
                await dbContext.SaveChangesAsync();                
                await ReportRecords();
            }
        }

        private async Task ProcessMatches(CakeDBContext dbContext)
        {
            var riotApi = RiotGamesApi.NewInstance(
                new RiotGamesApiConfig.Builder(ConfigurationManager.AppSettings["ApiKey"])
                {
                    Retries = 5
                }.Build()
            );

            cache.Members = await dbContext.Members.ToListAsync();

            foreach (var member in cache.Members)
            {
                var matchIds = await riotApi.MatchV5().GetMatchIdsByPUUIDAsync(
                    RegionalRoute.AMERICAS, member.Puuid, start: (int)member.LastProcessedGameTime, count: 5);

                foreach (var matchId in matchIds)
                {
                    var match = await riotApi.MatchV5().GetMatchAsync(RegionalRoute.AMERICAS, matchId);
                    if (IsQuorumMatch(match))
                    {
                        await CheckForRecords(dbContext, match);
                    }
                }
            }
        }

        private async Task CheckForRecords(CakeDBContext dbContext, Match match)
        {
            cache.TeamRecords = await dbContext.TeamRecords.ToListAsync();
            cache.SoloRecords = await dbContext.SoloRecords.ToListAsync();

            await CheckTeamRecords(match);

            foreach (var participant in match.Info.Participants.Where(x => cache.MemberPuuidSet.Contains(x.Puuid)))
            {
                await CheckSoloRecords(participant);
                await CheckPentakills(dbContext, participant);
            }
        }

        private async Task CheckTeamRecords(Match match, DbCache cache)
        {

        }

        private async Task CheckSoloRecords(Participant participant)
        {

        }

        private async Task CheckPentakills(CakeDBContext dbContext, Participant participant)
        {
            for (int i = 0; i < participant.PentaKills; i++)
            {
                var pentakill = new Pentakill
                {
                    ChampionId = (int)participant.ChampionId,
                    MemberId = cache.Members.Single(x => x.Puuid == participant.Puuid).Id,
                    Time = DateTime.UtcNow
                };

                dbContext.Pentakills.Add(pentakill);
            }
        }

        private bool IsQuorumMatch(Match match)
        {
            return match.Metadata.Participants.Intersect(cache.MemberPuuidSet).Count() >= MinimumParticipantCountForQuorum;
        }

        private async Task ReportRecords()
        {
            // TODO
        }
    }
}
