using System;
using Xunit;

using BellExperiments;

/// <summary> Helper methods for Unit Tests for my Bell Experiments. </summary>
namespace BellExperiments
{
    public static class Helpers
    {
        /// <summary> The numebr of trials in each loop. </summary>
        public const int num_trials = 1000;

        /// <summary> Helper method to calculate correlation on chosen axes. </summary>
        /// <param name="A"> The spin axis for Alice. </param>
        /// <param name="B"> The spin axis for Bob. </param>
        /// <param name="count"> The optional number of tests to run. Default 1000. </param>
        public static double CorrelateElectronsOnAxes(SPIN_AXIS A, SPIN_AXIS B, int count=1000)
        {
            Electron one = new Electron();
            Electron two = new Electron();

            double count_same = 0.0;
            double count_diff = 0.0;
            double count_all = 0.0;

            for(int i=0; i<count; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                int isA = one.Spin(A);
                int isB = two.Spin(B);

                count_all += 1.0;
                if (isA == isB)
                    count_same += 1.0;
                else
                    count_diff += 1.0;
            }

            // sadly, I forget where I saw this equation for calculating the correlation.
            //  usually it is more like:
            //
            //    C = (num_pp + num_mm - num_pm - num_mp) / (num_pp + num_mm + num_pm + num_mp)
            //
            //  but this is arithmetically equivalent. ( when strictly using real numbers. )
            //
            return (count_same - count_diff) / count_all;
        }

        /// <summary>
        /// Tests photons on two axes looking for specific goals.
        /// Generates the counts of that axis meeting that goal.
        /// </summary>
        /// <param name="A"> The angle Alice want to measure against. </param>
        /// <param name="goalA"> This is <c>true</a> if Alice is looking for successful test
        /// on this polarization angle. A <false> if Alice is looking for a failure on
        /// this polarization angle. </param>
        /// <param name="countA"> This <c>out</c> variable is set to the number of tests that
        /// matched Alice's goal. </param>
        /// <param name="B"> The angle Bob want to measure against. </param>
        /// <param name="goalB"> This is <c>true</a> if Bob is looking for successful test
        /// on this polarization angle. A <false> if Bob is looking for a failure on
        /// this polarization angle. </param>
        /// <param name="countB"> This <c>out</c> variable is set to the number of tests that
        /// matched Bob's goal. </param>
        /// <param name="count"> The optional number of trials to run,
        /// defaults to the <see cref="num_trials"/> constant. </param>
        public static void CountPhotonsSeparatelyOnAxes(double A, bool goalA, out double countA,
                                double B, bool goalB, out double countB,
                                int count = num_trials)
        {
            Photon one = new Photon();
            Photon two = new Photon();

            countA = 0;
            countB = 0;

            for(int i=0; i<count; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                // just count how many times each goal is met.
                //  no calculations involving both Alice and Bob.
                //
                if (one.Polarized(A) == goalA)
                    countA += 1;
                if (two.Polarized(B) == goalB)
                    countB += 1;
            }
        }
        /// <summary>
        /// Tests photons on two axes looking for specific goals.
        /// Generates the counts of that axis meeting that goal.
        /// </summary>
        /// <param name="A"> The angle Alice want to measure against. </param>
        /// <param name="goalA"> This is <c>true</a> if Alice is looking for successful test
        /// on this polarization angle. A <false> if Alice is looking for a failure on
        /// this polarization angle. </param>
        /// <param name="B"> The angle Bob want to measure against. </param>
        /// <param name="goalB"> This is <c>true</a> if Bob is looking for successful test
        /// on this polarization angle. A <false> if Bob is looking for a failure on
        /// this polarization angle. </param>
        /// <param name="countHits"> This <c>out</c> variable is set to the number of tests that
        /// matched both Alice's and Bob's goals. </param>
        /// <param name="count"> The optional number of trials to run,
        /// defaults to the <see cref="num_trials"/> constant. </param>
        public static void CountPhotonsTogetherOnAxes(double A, bool goalA,
                                double B, bool goalB,
                                out double countHits,
                                int count = num_trials)
        {
            Photon one = new Photon();
            Photon two = new Photon();

            countHits = 0;

            for(int i=0; i<count; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                // here we count only when both goals are met.
                //
                if ((one.Polarized(A) == goalA) && (two.Polarized(B) == goalB))
                    countHits += 1;
            }
        }
        /// <summary> Runs trials testing at the specified angles,
        /// counting the various results and then calculating the correltion
        /// between those two angles. </summary>
        /// <param name="A"> The angle that Alice want to test against. </param>
        /// <param name="B"> The angle that Bob want to test against. </param>
        /// <param name="count"> The optional number of tests to run.
        /// Defaults to the <see cref="num_trials"/> constant. </param>
        /// <returns> The correlation of photon slignment between these two angles. </summary>
        public static double CorrelatePhotonsTheHardWay(double A, double B, int count=num_trials)
        {
            Photon one = new Photon();
            Photon two = new Photon();

            double count_PP = 0.0;
            double count_PM = 0.0;
            double count_MP = 0.0;
            double count_MM = 0.0;
            double count_all = 0.0;

            for(int i=0; i<count; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                bool isA = one.Polarized(A);
                bool isB = two.Polarized(B);

                count_all += 1.0;
                if ( isA &&  isB) count_PP += 1;
                if ( isA && !isB) count_PM += 1;
                if (!isA &&  isB) count_MP += 1;
                if (!isA && !isB) count_MM += 1;
            }

            return Math.Abs(count_PP + count_PM) - Math.Abs(count_MP - count_MM);
        }
        /// <summary> Runs trials testing at the specified angles,
        /// counting the various results and then calculating the correltion
        /// between those two angles. </summary>
        /// <param name="A"> The angle that Alice want to test against. </param>
        /// <param name="B"> The angle that Bob want to test against. </param>
        /// <param name="count"> The optional number of tests to run.
        /// Defaults to the <see cref="num_trials"/> constant. </param>
        /// <returns> The correlation of photon alignment between these two angles. </summary>
        public static double CorrelatePhotonsOnAxes(double A, double B, int count=num_trials)
        {
            Photon one = new Photon();
            Photon two = new Photon();

            double count_same = 0.0;
            double count_diff = 0.0;
            double count_all = 0.0;

            for(int i=0; i<count; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                bool isA = one.Polarized(A);
                bool isB = two.Polarized(B);

                count_all += 1.0;
                if (isA == isB)
                    count_same += 1.0;
                else
                    count_diff += 1.0;
            }

            // sadly, I forget where I saw this equation for calculating the correlation.
            //  usually it is more like:
            //
            //    C = (num_pp + num_mm - num_pm - num_mp) / (num_pp + num_mm + num_pm + num_mp)
            //
            //  but this is arithmetically equivalent. ( when strictly using real numbers. )
            //
            return (count_same - count_diff) / count_all;
        }

        /// <summary> Runs trials testing at the specified angles,
        /// counting the various results and then calculating the probability
        /// of sameness between those two angles. </summary>
        /// <param name="A"> The angle that Alice want to test against. </param>
        /// <param name="B"> The angle that Bob want to test against. </param>
        /// <param name="count"> The optional number of tests to run.
        /// Defaults to the <see cref="num_trials"/> constant. </param>
        /// <returns> The probability of photon alignment being the same when
        /// measured at these two angles. </summary>
        public static double PhotonsPerSameOnAxes(double A, double B, int count=num_trials)
        {
            Photon one = new Photon();
            Photon two = new Photon();

            double count_same = 0.0;
            double count_all = 0.0;

            for(int i=0; i<count; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                bool isA = one.Polarized(A);
                bool isB = two.Polarized(B);

                count_all += 1.0;
                if (isA == isB)
                    count_same += 1.0;
            }

            return count_same / count_all;
        }
    }
}
