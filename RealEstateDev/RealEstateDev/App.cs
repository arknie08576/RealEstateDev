using RealEstateDev.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateDev
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;

        public App(IUserCommunication userCommunication)
        {
            _userCommunication = userCommunication;

        }
        public void Run()
        {
            _userCommunication.CommunicationWithUser();
        }
    }
}
