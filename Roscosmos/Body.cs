using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roscosmos
{
    public static class Body
    {
        private const double g = 9.81;
        private const double C = 0.15;
        private const double rho = 1.29;

        private static double Vx;
        private static double Vy;
        private static double K;

        public static double Y0;
        public static double V0;
        public static double Angle;
        public static double Area;
        public static double Mass;

        public static double FlightTime;
        public static double MaxHeight;
        public static double MaxDistance;

        public static void InitFlightParams()
        {
            Vx = V0 * Math.Cos(Angle * Math.PI / 180);
            Vy = V0 * Math.Sin(Angle * Math.PI / 180);
            K = 0.5 * C * Area * rho / Mass;

            FlightTime = (V0 * Math.Sin(Angle * Math.PI / 180) +
                          Math.Sqrt(Math.Pow(V0 * Math.Sin(Angle * Math.PI / 180), 2) +
                                    2 * g * Y0)) / g;
            MaxHeight = Y0 + Math.Pow(V0 * Math.Sin(Angle * Math.PI / 180), 2) / (2 * g);
            MaxDistance = V0 * Math.Cos(Angle * Math.PI / 180) * FlightTime;
        }

        public static void CalculatePosition(ref (double, double) pos, double dt)
        {
            Vx -= K * Vx * Math.Sqrt(Vx * Vx + Vy * Vy) * dt;
            Vy -= (g + K * Vy * Math.Sqrt(Vx * Vx + Vy * Vy)) * dt;

            pos.Item1 += Vx * dt;
            pos.Item2 += Vy * dt;
        }
    }
}
