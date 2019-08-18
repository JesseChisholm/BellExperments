using System;

namespace BellExperiments
{
    /// <summary> An enum for the various axes of spin. </summary>
public enum SPIN_AXIS {
    SPIN_AXIS_X,
    SPIN_AXIS_Y,
    SPIN_AXIS_Z
}

/// <summary> This generic base class holds the <c>hidden variables</c>
/// for particles that have 3-axes spin behavior. </summary>
public class Spinnable<T>
{
    /// <summary> The default constructor initializes a randomized particle. </summary>
    /// <remarks>
    /// A potential better model is:
    ///     1:  pick a uniform 3-vector for a new particle.
    ///     2:  the entangled particle diametrically opposite vector.
    ///             if cartesian, then [-x,-y,-z]
    ///             if spherical, then [-r,t,p] or [r,t+pi,p+pi] 
    ///     3:  measure spin on arbitrary angle
    ///         4:  calculate angle between measured axis and particle axis
    ///         5:  same hemisphere is +1
    ///         6:  other hemisphere is -1
    ///         7:  equator is random 50-50
    ///     8:  NOTE:   entangled particle will get (angle+pi) in step 4
    ///     9:          which, if not on the equator, is in the opposite hemisphere
    ///     10: NOTE:   testing against equator should use some sane EPSILON.
    /// </remarks>
    public Spinnable()
    {
        Randomize();
    }
    /// <summary> Any particle can be told to lose its entanglement. </summary>
    public void Randomize()
    {
        _spin_x = RandomSpin();
        _spin_y = RandomSpin();
        _spin_z = RandomSpin();
    }
    #region hidden variable fields
    private int _spin_x;
    private int _spin_y;
    private int _spin_z;
    #endregion hidden variable fields
    /// <summary> Any class of pseudo random generator will do. </summary>
    private Random random = new Random();
    /// <summary> A random spin value is always -1 or +1. </summary>
    private int RandomSpin()
    {
        return random.NextDouble() < 0.5 ? -1 : 1;
    }
    /// <summary> A predicate to test the value of this particles spin on this axis. </summary>
    /// <param name="axis"> The enum value for the axis to test. >/param>
    /// <returns> The return value is the +1 or -1 that is the spin value for this axis. </returns>
    /// <remarks> The spin on the other axes lose there entanglement. </remarks> 
    public int Spin(SPIN_AXIS axis) {
        switch(axis)
        {
            case SPIN_AXIS.SPIN_AXIS_X:
               // _spin_x = _spin_x;
                _spin_y = RandomSpin();
                _spin_z = RandomSpin();
                return _spin_x;
            case SPIN_AXIS.SPIN_AXIS_Y:
                _spin_x = RandomSpin();
                //_spin_y = _spin_y;
                _spin_z = RandomSpin();
                return _spin_y;
            case SPIN_AXIS.SPIN_AXIS_Z:
                _spin_x = RandomSpin();
                _spin_y = RandomSpin();
                //_spin_z = _spin_z;
                return _spin_z;
            default:
                // should never happen, but there is no spin on other axes. <):-)
                return 0;
        }
    }
     /// <summary>
    /// This method will force <c>this</c> particle to be entangled
    /// with the <paramref name="other"/> particle.
    /// </summary>
    /// <param name="other"> The other <c>Spinnable</c> particle to
    /// entangle<c>this</c> one with. </param>
    public void EntangleSpins(Spinnable<T> other)
    {
        // entangles spin particles always have opposite spin values.
        //
        _spin_x = - other._spin_x;
        _spin_y = - other._spin_y;
        _spin_z = - other._spin_z;
    }
}
}
