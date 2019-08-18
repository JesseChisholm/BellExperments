using System;
using Xunit;

using BellExperiments;

/// <summary> Unit Tests for my Bell Experiments. </summary>
namespace BellExperiments.Tests
{
    /// <summary> Unit Tests for my Bell Experiments that involve electrons. </summary>
    public class UnitTestElectrons
    {
        /// <summary> Verify that two randomized Electrons do not show
        /// any evidence of entanglement. </summary>
        [Fact]
        public void Verify_Randomized_Electrons_Not_Entangled()
        {
            Electron one = new Electron();
            Electron two = new Electron();

            int count_diff = 0; // we count anti-correlation
            int count_all = 0;
            // measure them both on the X axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.Randomize();

                ++count_all;
                if (one.Spin(SPIN_AXIS.SPIN_AXIS_X) != two.Spin(SPIN_AXIS.SPIN_AXIS_X))
                    ++count_diff;
            }
            // measure them both on the Y axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.Randomize();

                ++count_all;
                if (one.Spin(SPIN_AXIS.SPIN_AXIS_Y) != two.Spin(SPIN_AXIS.SPIN_AXIS_Y))
                    ++count_diff;
            }
            // measure them both on the Z axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.Randomize();

                ++count_all;
                if (one.Spin(SPIN_AXIS.SPIN_AXIS_Z) != two.Spin(SPIN_AXIS.SPIN_AXIS_Z))
                    ++count_diff;
            }

            Assert.False(count_diff == count_all, "Should not all be anti-correlated.");
        }
        /// <summary> Verify that entangled pairs of electrons DO display entanglement. </summary>
        [Fact]
        public void Verify_Entangled_Electrons_Entangled()
        {
            Electron one = new Electron();
            Electron two = new Electron();

            int count_diff = 0; // we count anti-correlation
            int count_all = 0;
            // measure them both on the X axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                ++count_all;
                if (one.Spin(SPIN_AXIS.SPIN_AXIS_X) != two.Spin(SPIN_AXIS.SPIN_AXIS_X))
                    ++count_diff;
            }
            // measure them both on the Y axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                ++count_all;
                if (one.Spin(SPIN_AXIS.SPIN_AXIS_Y) != two.Spin(SPIN_AXIS.SPIN_AXIS_Y))
                    ++count_diff;
            }
            // measure them both on the Z axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                ++count_all;
                if (one.Spin(SPIN_AXIS.SPIN_AXIS_Z) != two.Spin(SPIN_AXIS.SPIN_AXIS_Z))
                    ++count_diff;
            }

            Assert.True(count_diff == count_all, "Must all be anti-correlated.");
        }
        /// <summary Verify that repeated measurement ont he same axis do not destroy entanglement. </summary>
        [Fact]
        public void Verify_Entangled_Electrons_Maintain_Entanglement()
        {
            Electron one = new Electron();
            Electron two = new Electron();

            int count_diff = 0;
            int count_all = 0;
            // measure them both on the X axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                // measure them both on the X axis - a bunch of times!
                for(int j=0; j<1000; ++j)
                {
                    ++count_all;
                    if (one.Spin(SPIN_AXIS.SPIN_AXIS_X) != two.Spin(SPIN_AXIS.SPIN_AXIS_X))
                        ++count_diff;
                }
            }
            // measure them both on the Y axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                // measure them both on the Y axis - a bunch of times!
                for(int j=0; j<1000; ++j)
                {
                    ++count_all;
                    if (one.Spin(SPIN_AXIS.SPIN_AXIS_Y) != two.Spin(SPIN_AXIS.SPIN_AXIS_Y))
                        ++count_diff;
                }
            }
            // measure them both on the Z axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                // measure them both on the Z axis - a bunch of times!
                for(int j=0; j<1000; ++j)
                {
                    ++count_all;
                    if (one.Spin(SPIN_AXIS.SPIN_AXIS_Z) != two.Spin(SPIN_AXIS.SPIN_AXIS_Z))
                        ++count_diff;
                }
            }

            Assert.True(count_diff == count_all, "Must allways be anti-correlated.");
        }
        /// <summary> Verify that measuring on one axis un-entangles the pair on other axes. </summary>
        [Fact]
        public void Verify_Entangled_Electrons_Decohere()
        {
            Electron one = new Electron();
            Electron two = new Electron();

            int count_diff = 0;
            int count_all = 0;
            // measure them both on the X axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                ++count_all;
                bool measure_once = one.Spin(SPIN_AXIS.SPIN_AXIS_Z) != two.Spin(SPIN_AXIS.SPIN_AXIS_Z);
                // measurement on Z should break entanglement on X
                if (one.Spin(SPIN_AXIS.SPIN_AXIS_X) != two.Spin(SPIN_AXIS.SPIN_AXIS_X))
                    ++count_diff;
            }
            // measure them both on the Y axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                ++count_all;
                bool measure_once = one.Spin(SPIN_AXIS.SPIN_AXIS_X) != two.Spin(SPIN_AXIS.SPIN_AXIS_X);
                // measurement on X should break entanglement on Y
                if (one.Spin(SPIN_AXIS.SPIN_AXIS_Y) != two.Spin(SPIN_AXIS.SPIN_AXIS_Y))
                    ++count_diff;
            }
            // measure them both on the Z axis
            for(int i=0; i<1000; ++i)
            {
                one.Randomize();
                two.EntangleSpins(one);

                ++count_all;
                bool measure_once = one.Spin(SPIN_AXIS.SPIN_AXIS_Y) != two.Spin(SPIN_AXIS.SPIN_AXIS_Y);
                // measurement on Y should break entanglement on Z
                if (one.Spin(SPIN_AXIS.SPIN_AXIS_Z) != two.Spin(SPIN_AXIS.SPIN_AXIS_Z))
                    ++count_diff;
            }

            Assert.False(count_diff == count_all, "Must not always be anti-correlated.");
        }

        /// <summary> Verify that the same axis is always anti-correlated. </summary>
        [Fact]
        public void Verify_Same_Axis_Always_Opposite()
        {
            double cor_XX = Helpers.CorrelateElectronsOnAxes(SPIN_AXIS.SPIN_AXIS_X, SPIN_AXIS.SPIN_AXIS_X);
            double cor_YY = Helpers.CorrelateElectronsOnAxes(SPIN_AXIS.SPIN_AXIS_Y, SPIN_AXIS.SPIN_AXIS_Y);
            double cor_ZZ = Helpers.CorrelateElectronsOnAxes(SPIN_AXIS.SPIN_AXIS_Z, SPIN_AXIS.SPIN_AXIS_Z);

            Assert.True(cor_XX == -1.0, "Should be perfectly anti-correlated.");
            Assert.True(cor_YY == -1.0, "Should be perfectly anti-correlated.");
            Assert.True(cor_ZZ == -1.0, "Should be perfectly anti-correlated.");
        }
        /// <summary> Check the basic Bell's Inequality. </summary>
        [Fact]
        public void Check_Bell_Inequality()
        {
            double cor_XY = Helpers.CorrelateElectronsOnAxes(SPIN_AXIS.SPIN_AXIS_X, SPIN_AXIS.SPIN_AXIS_Y);
            double cor_YZ = Helpers.CorrelateElectronsOnAxes(SPIN_AXIS.SPIN_AXIS_Y, SPIN_AXIS.SPIN_AXIS_Z);
            double cor_ZX = Helpers.CorrelateElectronsOnAxes(SPIN_AXIS.SPIN_AXIS_Z, SPIN_AXIS.SPIN_AXIS_X);

            // see: https://en.wikipedia.org/wiki/Bell%27s_theorem#Original_Bell's_inequality
            //
            double cor_all = cor_XY - cor_YZ - cor_ZX;

            bool spooky = (cor_all > 1.0);

            Assert.True(spooky, "Classic when <= 1.");
        }
    }
}
