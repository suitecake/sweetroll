using CakeAF.Entity;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using RiotSharp;
using RiotSharp.Misc;
using RiotSharp.Endpoints.MatchEndpoint;

namespace CakeAF
{
    internal class GameProcessor
    {
        private TeamRecordService teamRecordService = null;

        private readonly DbCache cache = new DbCache();
        
        private const int MinimumParticipantCountForQuorum = 4;

        private readonly string riotApiKey = ConfigurationManager.AppSettings["ApiKey"];

        [FunctionName("GameProcessor")]
        public async Task Run([TimerTrigger("0 */3 * * * *")] TimerInfo myTimer, ILogger log)
        {
            using (var dbContext = new CakeDBContext())
            {
                var games = ProcessGameData(dbContext);
            }
        }

        private async Task ProcessGameData(CakeDBContext dbContext)
        {
            var riotApi = RiotApi.GetDevelopmentInstance(riotApiKey);

            cache.Members = await dbContext.Members.ToListAsync();

            foreach (var member in cache.Members)
            {
                var matchIds = await riotApi.Match.GetMatchListAsync(Region.Na,
                    member.Puuid,
                    start: member.LastProcessedGameTime,
                    count: 5);

                foreach (var matchId in matchIds)
                {
                    var match = await riotApi.Match.GetMatchAsync(Region.Na, matchId);
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

        private async Task CheckTeamRecords(Match match)
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
