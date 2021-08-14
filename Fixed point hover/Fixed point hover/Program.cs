using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using KRPC.Client;
using KRPC.Client.Services.KRPC;
using KRPC.Client.Services.SpaceCenter;

namespace Fixed_point_hover
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var connection = new Connection(
                  name: "My Example Program",
                  address: IPAddress.Parse("127.0.0.1"),
                  rpcPort: 50000,
                  streamPort: 50001))
            {
                var krpc = connection.KRPC();
                Console.WriteLine(krpc.GetStatus().Version);
            }
            var conn = new Connection("Sub-orbital flight");

            var vessel = conn.SpaceCenter().ActiveVessel;

            vessel.AutoPilot.TargetPitchAndHeading(90, 90);
            vessel.AutoPilot.Engage();
            vessel.Control.Throttle = 1;
            System.Threading.Thread.Sleep(1000);

            Console.WriteLine("Launch!");
            vessel.Control.ActivateNextStage();
        }
    }
}
