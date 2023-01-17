using RiotSharp.Endpoints.MatchEndpoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CakeAF
{
    /// <summary>
    /// Functions for determining whether team record has been set
    /// </summary>
    public class TeamRecordService
    {
        private readonly DbCache cache;

        public TeamRecordService(DbCache cache)
        {
            this.cache = cache;
        }

        public async Task Check(Match match)
        {
            Team ourTeam = match.Info.Teams.Single(x => x.TeamId == match.Info.Participants.First(x => cache.MemberPuuidSet.Contains(x.Puuid)).TeamId);
            
            await CheckQuickestVictory(match, ourTeam);
        }

        private async Task CheckQuickestVictory(Match match, Team ourTeam)
        {
            if (ourTeam.Win)
            {
                var record = cache.TeamRecords.Single(x => x.Id == Constants.TeamRecordIds.QuickestVictory);
                if (record.Value > match.Info.GameDuration)
                {

                }
            }
            if ((x => x.TeamId == teamId).Win)
            {

            }
            // if is victory, check 
        }
    }
}
