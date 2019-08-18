# BellExperments
Explorations in Bell's Inequality

In part, these are models of various `Bell Inequality` experiments I've read about.

But also, in part, they are test of a few `hidden variable` models in an attempt to re-create some of the QM behaviour.

## Polarizable<T>
This base generic class handles all the `hidden variables` involved in polarization.

It is woefully inadequate to describe **all** of the QM behavior involving polarization.
For instance, it only describes `linear polarization` not `circular polarization`, let alone `elliptical polarization`.

But simple tests (such as the `0|120|240` polarization tests) it is adequate for.

More rigorous tests (like the CHSH) ... not so much.

## Spinnable<T>
This base generic class handles all the `hidden variables` involved in `spin` around the three spacial axes.

I suspect it is inadequate to describe **all** the QM behavior involving spin.
But most Bell Inequality Tests documented are about photons, and only a few about electrons.

**CAVEATS**:
* My spin model requires measurement on exactly the X, Y or Z axis.
  * A better model would allow measurement on any rotated axis.

## Unit Tests
I have chosen to use the unit test structure to validate and test my models.

First, there are those that **verify** that the model meets the bare minimum requirements for the behavior of random particles and entangled particles.

Then there are those which try to **check** my models against some Bell test Experiment I found documented out in the real world.

## Conclusions
Well, as of 2019.08.18, when I documented all this and put it up on github for the first time. <):-)

* My `hidden variable` models for `spin` and `linear polarization` and _how measurements are done_ adequately handles:
  * randomized particles behave as randomized particles ought to.
  * entangled particles behave (in simple tests) as entangled particles ought to.
    * Measuring spin on one axis is stable no matter how many time you measure it, nor on which entangled particle you measure it.
    * Measuring spin on one axis destroys entanglement on the other two axes.
	* Measuring polarization on any axis has 50% chance of passing.
    * If it does pass, then it also changes the particle's polarization to that axis.
* My `hidden variable` models are **not** adequate for **all** demonstrated QM behavior. (not unexpected)
  * I would actually have been quite surprised if all my tests had passed.

### CAVEATS:
* I do not bother with **Complex** values or arithmetic.
  * These models of `hidden variables` didn't happen to require it.
  * If I develop other models, they might.  We'll have to see.
* It is possible that I have not correctly coded some of the tests.
  * I will review both those that passed and those that failed to be sure I have modeled the relevent math properly.

## Further Reading
* [Testing Bell’s Theorem with Circular Polarization](https://www.scirp.org/journal/PaperInformation.aspx?PaperID=71970)
* [Disproof of Bell's Theorem](http://libertesphilosophica.info/blog/lpmain/)
  * Reference to a book that can be requested from the Library of Congress, or downloaded for $38.

