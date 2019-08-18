using System;
using Xunit;

using BellExperiments;

/// <summary> Unit Tests for my Bell Experiments. </summary>
namespace BellExperiments.Tests
{
    /// <summary> Unit Tests for my Bell Experiments that involve photons. </summary>
    public class UnitTestPhotons
    {
        /// <summary> The numebr of trials in each loop. </summary>
        public const int num_trials = 1000;
        /// <summary> Some tests find 90 degrees (in radians) convenient. </summary>
        public static double ninety = (90.0 / (180.0 / Math.PI));
        /// <summary> Some tests find 60 degrees (in radians) convenient. </summary>
        public static double sixty = (60.0 / (180.0 / Math.PI));
        /// <summary> Some tests find 22.5 degrees (in radians) convenient. </summary>
        public static double half_45 = (22.5 / (180.0 / Math.PI));


        /// <summary> Verify that randomized photons do not basic show entanglement properties. </summary>
        [Fact]
        public void Verify_Randomized_Photons_Not_Entangled()
        {
            Photon one = new Photon();
            Photon two = new Photon();

            int count_same = 0;
            int count_all = 0;
            // measure two random photons at 0 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.Randomize();

                ++count_all;
                if (one.Polarized(0 * sixty) == two.Polarized(0 * sixty))
                    ++count_same;
            }
            // measure two random photons at 60 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.Randomize();

                ++count_all;
                if (one.Polarized(1 * sixty) == two.Polarized(1 * sixty))
                    ++count_same;
            }
            // measure two random photons at 120 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.Randomize();

                ++count_all;
                if (one.Polarized(2 * sixty) == two.Polarized(2 * sixty))
                    ++count_same;
            }

            Assert.False(count_same == count_all, "Should not all be correlated.");
        }
        /// <summary> Verify that entangled photons do show basic entanglement properties. </summary>
        [Fact]
        public void Verify_Entangled_Photons_Entangled()
        {
            Photon one = new Photon();
            Photon two = new Photon();

            int count_same = 0;
            int count_all = 0;
            // measure two entangled photons at 0 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                ++count_all;
                if (one.Polarized(0 * sixty) == two.Polarized(0 * sixty))
                    ++count_same;
            }
            // measure two entangled photons at 60 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                ++count_all;
                if (one.Polarized(1 * sixty) == two.Polarized(1 * sixty))
                    ++count_same;
            }
            // measure two entangled photons at 120 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                ++count_all;
                if (one.Polarized(2 * sixty) == two.Polarized(2 * sixty))
                    ++count_same;
            }

            Assert.True(count_same == count_all, "Must all be correlated.");
        }
        /// <summary> Verify that multiple measurements on the same axis to not destroy entanglment. </summary>
        [Fact]
        public void Verify_Entangled_Photons_Maintain_Entanglment()
        {
            Photon one = new Photon();
            Photon two = new Photon();

            int count_same = 0;
            int count_all = 0;
            // measure two entangled photons at 0 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                // measure two entangled photons at 0 degrees, lots of times.
                for(int j=0; j<100; ++j)
                {
                    ++count_all;
                    if (one.Polarized(0 * sixty) == two.Polarized(0 * sixty))
                        ++count_same;
                }
            }
            // measure two entangled photons at 60 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                // measure two entangled photons at 60 degrees, lots of times.
                for(int j=0; j<100; ++j)
                {
                    ++count_all;
                    if (one.Polarized(1 * sixty) == two.Polarized(1 * sixty))
                        ++count_same;
                }
            }
            // measure two entangled photons at 120 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                // measure two entangled photons at 120 degrees, lots of times.
                for(int j=0; j<100; ++j)
                {
                    ++count_all;
                    if (one.Polarized(2 * sixty) == two.Polarized(2 * sixty))
                        ++count_same;
                }
            }

            Assert.True(count_same == count_all, "Must always be correlated.");
        }
        /// <summary> Verify that measurement on a second angle is no longer entangled. </summary>
        [Fact]
        public void Verify_Entangled_Photons_Decohere()
        {
            Photon one = new Photon();
            Photon two = new Photon();

            int count_same = 0;
            int count_all = 0;
            // measure two entangled photons at 120 degrees, then test at 0 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                ++count_all;
                bool measure_once = one.Polarized(2 * sixty);
                // measuring at 120 randomized 0
                if (one.Polarized(0 * sixty) == two.Polarized(0 * sixty))
                    ++count_same;
            }
            // measure two entangled photons at 0 degrees, then test at 60 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                ++count_all;
                bool measure_once = one.Polarized(0 * sixty);
                // measuring at 0 randomized 60
                if (one.Polarized(1 * sixty) == two.Polarized(1 * sixty))
                    ++count_same;
            }
            // measure two entangled photons at 60 degrees, then test at 120 degrees.
            for(int i=0; i<num_trials; ++i)
            {
                one.Randomize();
                two.EntanglePolarization(one);

                ++count_all;
                bool measure_once = one.Polarized(1 * sixty);
                // measuring at 60 randomized 120
                if (one.Polarized(2 * sixty) == two.Polarized(2 * sixty))
                    ++count_same;
            }

            Assert.False(count_same == count_all, "Must not always be correlated.");
        }

        /// <summary> Verify that measuring on the same axis is always the same result. </summary>
        [Fact]
        public void Verify_Same_Axis_Always_Same()
        {
            double cor_XX = Helpers.CorrelatePhotonsOnAxes(0 * sixty, 0 * sixty);
            double cor_YY = Helpers.CorrelatePhotonsOnAxes(1 * sixty, 1 * sixty);
            double cor_ZZ = Helpers.CorrelatePhotonsOnAxes(2 * sixty, 2 * sixty);

            Assert.True(cor_XX == 1.0, "Should be perfectly correlated.");
            Assert.True(cor_YY == 1.0, "Should be perfectly correlated.");
            Assert.True(cor_ZZ == 1.0, "Should be perfectly correlated.");
        }
        /// <summary> Check basic Bell Inequality. </summary>
        [Fact]
        public void Check_Bell_Inequality_Correlation()
        {
            double cor_XY = Helpers.CorrelatePhotonsOnAxes(0 * sixty, 1 * sixty);
            double cor_YZ = Helpers.CorrelatePhotonsOnAxes(1 * sixty, 2 * sixty);
            double cor_ZX = Helpers.CorrelatePhotonsOnAxes(2 * sixty, 0 * sixty);

            double cor_all = cor_XY - cor_YZ - cor_ZX;

            // The Bell Inequality says that:
            //  using hidden variables, the value must always be less than or equal to 1.0.
            // Therfore, to demonstrate QM behavior, it must be greater than one.
            //
            Assert.True(cor_all > 1.0, "Classical when <= 1.");
        }
        /// <summary> Check the CHSH variant of the Bell Inequality. </summary>
        [Fact]
        public void Check_Chsh_Inequality_Correlation()
        {
            double cor_AB = Helpers.CorrelatePhotonsOnAxes(0 * half_45, 1 * half_45);
            double cor_Ab = Helpers.CorrelatePhotonsOnAxes(0 * half_45, 3 * half_45);
            double cor_aB = Helpers.CorrelatePhotonsOnAxes(2 * half_45, 1 * half_45);
            double cor_ab = Helpers.CorrelatePhotonsOnAxes(2 * half_45, 3 * half_45);

            double cor_S = cor_AB - cor_Ab + cor_aB + cor_ab;
            //
            // with pathological correlations, the maximal range is [ -4 .. 4 ]

            // If it is numerically greater than 2 it has infringed the CHSH inequality
            //  and the experiment is declared to have supported the QM (Quantum Mechanics)
            //  prediction and ruled out all local hidden variable theories.

            // CLASSICAL: Assert.InRange(cor_S, -2.0, 2.0);
            Assert.NotInRange(cor_S, -2.0, 2.0);
        }
        /// <summary> Check the CH74 variant of the Bell Inequality. </summary>
        [Fact]
        public void Check_Ch74_Inequality_Correlation()
        {
            double tA = 0 * ninety; // Alice's first filter choice
            double ta = 2 * ninety; // Alice's second filter choice
            double tB = 1 * ninety; // Bob's first filter choice
            double tb = 3 * ninety; // Bob's second filter choice

            double count_A__p_ = 0.0;   // count Alice's first choice, don't care Bob.
            double count__b__p = 0.0;   // count Bob's second choice, don't care Alice.
            double count_AB_pp = 0.0;   // count with both first choices.
            double count_Ab_pp = 0.0;   // count Alice's first and Bob's second.
            double count_aB_pp = 0.0;   // count Alice's second and Bob's first.
            double count_ab_pp = 0.0;   // count with both second choices.
            //
            // CAVEAT: Becuase my helper methdos were originally written for a simpler
            //  test, to use them I have to run separate tests for each thing phase.
            //  Statistically, this should be OK.
            //  Or, I could write a new helper that counted everything up in one pass.
            //
            Helpers.CountPhotonsSeparatelyOnAxes(tA, true, out count_A__p_, tb, true, out count__b__p);
            Helpers.CountPhotonsTogetherOnAxes(tA, true, tB, true, out count_AB_pp);
            Helpers.CountPhotonsTogetherOnAxes(tA, true, tb, true, out count_Ab_pp);
            Helpers.CountPhotonsTogetherOnAxes(ta, true, tB, true, out count_aB_pp);
            Helpers.CountPhotonsTogetherOnAxes(ta, true, tb, true, out count_ab_pp);

            // CH74?
            // S = (N(a, b) − N(a, b′) + N(a′, b) + N(a′, b′) − N(a′, ∞) − N(∞, b)) / N(∞, ∞),
            //
            double S = (count_AB_pp - count_Ab_pp + count_aB_pp + count_ab_pp - count_A__p_ - count__b__p) / num_trials;

            // If S exceeds 0 then the experiment is declared to have infringed Bell's inequality
            //  and hence to have "refuted local realism".

            // assuming pathological counts, 6 is the highest magnitude possible.
            //
            // CLASSICAL: Assert.InRange(S, -6.0, 0.0);
            //
            Assert.InRange(S, 0.0, 6.0);
        }
        /// <summary> Check basic Bell Inequality using percentages instad of correlation. </summary>
        [Fact]
        public void Check_Bell_Inequality_Percentage()
        {
            double percent_XY = Helpers.PhotonsPerSameOnAxes(0 * sixty, 1 * sixty);
            double percent_YZ = Helpers.PhotonsPerSameOnAxes(1 * sixty, 2 * sixty);
            double percent_ZX = Helpers.PhotonsPerSameOnAxes(2 * sixty, 0 * sixty);

            // QM's prediction of .250
            // Local Hidden Variables scenario: at least .333

            double one_third = 1.0 / 3.0;
            double one_quarter = 1.0 / 4.0;

            // bool spooky_XY = percent_XY < one_third;
            // bool spooky_YZ = percent_YZ < one_third;
            // bool spooky_ZX = percent_ZX < one_third;

            // Assert.True(spooky_XY, "Oops! Classical in XY.");
            // Assert.True(spooky_YZ, "Oops! Classical in YZ.");
            // Assert.True(spooky_ZX, "Oops! Classical in ZX.");

            Assert.InRange(percent_XY, one_quarter, one_third);
            Assert.InRange(percent_YZ, one_quarter, one_third);
            Assert.InRange(percent_ZX, one_quarter, one_third);
        }
    }
}
