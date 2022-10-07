using ProfileService.BLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfileService.BLL.Interfaces
{
    public interface IBattleResultCounter : IDepositer, IWithdrawer
    {
        public void CountVictory(Profile profile, int pointsAmount);

        public void CountLose(Profile profile, int pointsAmount);
    }
}
