using System;

namespace BellExperiments
{
    /// <summary>
    /// The Polarizable generic base class defines the minimum <c>hidden variables</c>
    /// necessary for <c>linear polarization</c>.
    /// </summary>
    /// <remarks>
    /// To handle <c>elliptical polarization</c> I would need to:
    /// (1) track the phase shift between the electromatic and magnetic portions of the wave.
    /// (2) code a way to test linear alignment on an elliptical particle.
    /// (3) code a way for the filter to have phase shift between portions.
    ///     e.g. is this a linear filter, a quarter-wave filter, or what?
    /// </remarks>
    public class Polarizable<T>
    {
        /// <summary> The default constructor creates a randomized particle. </summary>
        public Polarizable()
        {
            _alignment = RandomAlignment();
        }
        /// <summary> Any Polarizable particle may be randomized to lose its entanglement. </summary>
        public void Randomize() 
        {
            _alignment = RandomAlignment();
        }
        /// <summary> THis is the private `hidden variable` in this model. </summary>
        private double _alignment;
        /// <summary> This threshold gives me the 50% I was looking for. </summary>
        private double _threshold = Math.Sqrt(2.0)/2.0;
        /// <summary> Any class of pseudo random number generator will do. <summary>
        private Random random = new Random();
        /// <summary> Create the angle of linear polarization just anywhere. </summary>
        /// <returns> A new angle of alignment in the range [ 0.0 .true.true 2 PI ].
        private double RandomAlignment()
        {
            return random.NextDouble() * 2.0 * Math.PI;
        }
        /// <summary> A predicate to test if this particle is polarized on this angle. </summary>
        /// <param name="angle"> The alignment angle to test against. </param>
        /// <returns> The return value is <c>true</c> if this particle passes through
        /// a filter at this angle, and <c>false</c> if it does not pass through a
        /// filter at this angle. </returns>
        /// <remarks> After this test, this particle will be perfectly aligned to
        /// this <paramref name="angle"/>. </remarks>
        public bool Polarized(double angle)
        {
            double ca = angle;
            double pa = _alignment;
            _alignment = angle;
            double diff = Math.Abs(Math.Cos(Math.Abs(ca-pa)));
            return diff < _threshold;

        }
        /// <summary>
        /// This method will force <c>this</c> particle to be entangled
        /// with the <paramref name="other"/> particle.
        /// </summary>
        /// <param name="other"> The other <c>Polarizable</c> particle to
        /// entangle<c>this</c> one with. </param>
        public void EntanglePolarization(Polarizable<T> other)
        {
            // entangled polarizable particles always have the same alignment.
            //
            _alignment = other._alignment;
        }
    }
}