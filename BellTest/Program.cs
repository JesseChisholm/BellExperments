using System;

namespace BellExperiments
{
    /// <summary>
    /// A simple program to test some <c>hidden variable</c> ideas.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A Bell's Inequality Experiment!");

            Console.WriteLine("");
            Console.WriteLine("1.p: Bell Tri-Axis Photon Polarization Test");
            BellTriAxisPhotonPolarizationTest();
            Console.WriteLine("");
            Console.WriteLine("1.e: Bell Tri-Axis Electron Spin Test");
            BellTriAxisElectronSpinTest();

            Console.WriteLine("");
            Console.WriteLine("2.p: Bell CHSH Photon Polarization Test");
            BellChshPhotonTest();
        }
        /// <summary> Any class of pseudo random generator will do. </summary>
        private static Random random = new Random();
        /// <summary> Generates a random integer in the range [0 .. count)
        /// which is to say, inclusive of zero, but exclusive of count. </summary>
        /// <returns> The return valus is from zero inclusive
        /// to <paramref name="count"/> exclusive. </returns>
        static int RandomIndex(int count)
        {
            return random.Next(count-1);
        }
        /// <summary>
        /// Run the three axes Bell Inequality Test against Polarized Photons.
        /// </summary>
        /// <remarks>
        /// See https://en.wikiversity.org/wiki/Bell%27s_theorem
        /// </remarks>
        internal static void BellTriAxisPhotonPolarizationTest()
        {
            // 30 degress, in radians, is convenient to calculate the three angles.
            //
            double thirty = 30.0 / (180.0 / Math.PI);
            double[] filters = {
                2 * thirty, // ===  60 == 240
                3 * thirty, // ===  90 == 270
                4 * thirty  // === 120 == 300
            };
            // every test works with two photons.
            Photon one = new Photon();
            Photon two = new Photon();
            // various counts of the tests that pass the alignment filter.
            int count_A = 0;
            int count_B = 0;
            int count_AB = 0;   // as in both
            int count_all = 0;  // as in all tests run.
            for(int i=0; i<1000; ++i)
            {
                // force them to pick different filters
                //  0 + 1, 1 + 2, 2 + 0
                double Alice = filters[(i+0)%3];
                double Bob = filters[(i+1)%3];

                // each test starts with a randomized particle
                one.Randomize();
                // and a particle in _perfect_ entanglement with it.
                two.EntanglePolarization(one);

                bool answer_A = one.Polarized(Alice);
                bool answer_B = two.Polarized(Bob);
                count_all++;
                count_AB += (answer_A&&answer_B)?1:0;
                count_A += (answer_A)?1:0;
                count_B += (answer_B)?1:0;
            }
            double percent_A = ((count_A*100.0)/(count_all+0.0));
            double percent_B = ((count_B*100.0)/(count_all+0.0));
            double percent_AB = ((count_AB*100.0)/(count_all+0.0));
            bool spooky = (percent_AB < 33.33) && (percent_AB >= 25.00);
            Console.WriteLine(String.Format("{0} of {1} Agreement, for {2}%  == {3}",
                count_AB, count_all, percent_AB,
                spooky ? "Spooky" : "Classic"));
            Console.WriteLine(">=33.333 suggests classic hidden variables.");
            Console.WriteLine("<33.333 >=25.000 suggests spooky action at a distance.");
        }
        /// <summary>
        /// Run the three axes Bell Inequality Test against Sinnable Electrons.
        /// </summary>
        /// <remarks>
        /// See https://en.wikipedia.org/wiki/Sakurai%27s_Bell_inequality
        /// </remarks>
        internal static void BellTriAxisElectronSpinTest()
        {
            SPIN_AXIS[] filters = {
                SPIN_AXIS.SPIN_AXIS_X,
                SPIN_AXIS.SPIN_AXIS_Y,
                SPIN_AXIS.SPIN_AXIS_Z
            };
            // every test works with two photons.
            Electron one = new Electron();
            Electron two = new Electron();
            // various counts of the tests that pass the spin question.
            int count_same = 0;
            int count_same_XY = 0;
            int count_same_YZ = 0;
            int count_same_ZX = 0;
            int count_all = 0;
            int count_all_XY = 0;
            int count_all_YZ = 0;
            int count_all_ZX = 0;
            for(int i=0; i<1000; ++i)
            {
                // force them to pick different filters
                //  X + Y, Y + Z, Z + X
                int index_A = (i+0) % 3;
                int index_B = (i+1) % 3;
                SPIN_AXIS A = filters[index_A];
                SPIN_AXIS B = filters[index_B];

                // each test starts with a randomized particle
                one.Randomize();
                // and a particle in _perfect_ entanglement with it.
                two.EntangleSpins(one);

                int answer_A = one.Spin(A);
                int answer_B = two.Spin(B);
                count_all++;
                // so we can know how many on each filter.
                switch(index_A)
                {
                    case 0: ++count_all_XY; break;
                    case 1: ++count_all_YZ; break;
                    case 2: ++count_all_ZX; break;
                }
                // count when they are in agreement.
                if (answer_A==answer_B)
                {
                    ++count_same;
                    // and again, on each filter.
                    switch(index_A)
                    {
                        case 0: ++count_same_XY; break;
                        case 1: ++count_same_YZ; break;
                        case 2: ++count_same_ZX; break;
                    }
                }
            }
            // correlation regardless of axes choice
            double CE = (2.0*count_same-count_all)/count_all;
            // correlation on specific axes choice
            double Cxy = (2.0*count_same_XY-count_all_XY)/count_all_XY;
            double Cyz = (2.0*count_same_YZ-count_all_YZ)/count_all_YZ;
            double Czx = (2.0*count_same_ZX-count_all_ZX)/count_all_ZX;
            // estimate of correlation combined.
            double Ch = Cxy - Cyz - Czx;
            bool spooky = (Ch >= 1.0);
            Console.WriteLine(String.Format("Ce={0} Ch={1}, from {2} == {3}",
                CE, Ch, count_all,
                spooky ? "Spooky" : "Classic"));
        }
        /// <summary> A convenience class to hold sets of related counts. </summary>
            internal class tracks {
                public tracks(){yy=yn=ny=nn=0;}
                public int yy;
                public int yn;
                public int ny;
                public int nn;
                /// <summary> Calculate the correlation of these counts. </summary>
                public double Value()
                {
                    double N = yy - yn - ny + nn;
                    double D = yy + yn + ny + nn;
                    return (D == 0) ? 0.0 : (N/D);
                }
            }
        /// <summary>
        /// Run the CHSH Bell Inequality Test against Polarized Photons.
        /// </summary>
        /// <remarks>
        /// </remarks>
        internal static void BellChshPhotonTest()
        {
            // just a convenient angle to get all others from.
            double fortyfive = 45.0 / (180.0 / Math.PI);
            double[] filters_A = {
                0 * fortyfive, // ===   0
                1 * fortyfive  // ===  45
            };
            double[] filters_B = {
                0.5 * fortyfive, // ===  22.5
                1.5 * fortyfive  // ===  67.5
            };
            // track counts baed on which pair of angles are used.
            tracks[] E = new tracks[4] {
            new tracks(),
            new tracks(),
            new tracks(),
            new tracks()
            };
            // our two photons.
            Photon one = new Photon();
            Photon two = new Photon();
            // and various individual counts regardless of test angle.
            int count_A_y = 0;
            int count_A_n = 0;
            int count_B_y = 0;
            int count_B_n = 0;
            int count_all = 0;
            for(int i=0; i<1000; ++i)
            {
                // let Alice nad Bob pick random filter angles from their lists.
                int index_A = RandomIndex(2);
                int index_B = RandomIndex(2);
                // and calculat teh track number of this choice.
                int index_T = index_A * 2 + index_B;

                double A = filters_A[index_A];
                double B = filters_B[index_B];

                // each test has a randomized particle
                one.Randomize();
                // and a perfectly entangled particle
                two.EntanglePolarization(one);
                bool answer_A = one.Polarized(A);
                bool answer_B = two.Polarized(B);
                // now count up the results in their various tracks.
                if (answer_A) count_A_y++; else count_A_n++;
                if (answer_B) count_B_y++; else count_B_n++;
                if (answer_A && answer_B) E[index_T].yy++;
                if (answer_A && !answer_B) E[index_T].yn++;
                if (!answer_A && answer_B) E[index_T].ny++;
                if (!answer_A && !answer_B) E[index_T].nn++;
                count_all++;
            }
            // And sum the correlations of each track.
            double S = E[0].Value() - E[1].Value() + E[2].Value() + E[3].Value();
            Console.WriteLine(String.Format("CHSH of {0} == {1}",
                S,
                (S>2.0) ? "Spooky" : "Classic"));
            Console.WriteLine("<=2.0 suggests classic hidden variables.");
            Console.WriteLine("> 2.0 suggests spooky action at a distance.");
        }
    }
}
