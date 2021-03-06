using System;
using System.Numerics;
using AdventOfCode;

namespace Advent2019
{
    public class Challenge12 : Challenge
    {

        private struct Moon
        {
            public Vector3 Position;
            public Vector3 Velocity;

            public Moon(int x, int y, int z)
            {
                Position = new Vector3(x, y, z);
                Velocity = new Vector3();
            }
        }

        public override object Task1()
        {
            Moon[] moons = new Moon[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                string[] parts = input[i].Substring(1, input[i].Length - 2).Split(", ");
                moons[i] = new Moon(int.Parse(parts[0].Split('=')[1]),
                                    int.Parse(parts[1].Split('=')[1]),
                                    int.Parse(parts[2].Split('=')[1]));
            }

            for (int _ = 0; _ < 1000; _++)
            {
                SimulateStep(moons);
            }

            long energy = 0;
            for (int i = 0; i < moons.Length; i++)
            {
                energy += NRG(moons[i].Position) * NRG(moons[i].Velocity);
            }
            return energy;
        }

        private void SimulateStep(Moon[] moons)
        {
            //Gravity
            for (int i = 0; i < moons.Length - 1; i++)
            {
                for (int j = i + 1; j < moons.Length; j++)
                {
                    if (moons[i].Position.X > moons[j].Position.X)
                    {
                        moons[i].Velocity.X--;
                        moons[j].Velocity.X++;
                    }
                    else if (moons[i].Position.X < moons[j].Position.X)
                    {
                        moons[i].Velocity.X++;
                        moons[j].Velocity.X--;
                    }

                    if (moons[i].Position.Y > moons[j].Position.Y)
                    {
                        moons[i].Velocity.Y--;
                        moons[j].Velocity.Y++;
                    }
                    else if (moons[i].Position.Y < moons[j].Position.Y)
                    {
                        moons[i].Velocity.Y++;
                        moons[j].Velocity.Y--;
                    }

                    if (moons[i].Position.Z > moons[j].Position.Z)
                    {
                        moons[i].Velocity.Z--;
                        moons[j].Velocity.Z++;
                    }
                    else if (moons[i].Position.Z < moons[j].Position.Z)
                    {
                        moons[i].Velocity.Z++;
                        moons[j].Velocity.Z--;
                    }
                }
            }

            // Velocity
            for (int i = 0; i < moons.Length; i++)
            {
                moons[i].Position += moons[i].Velocity;
            }
        }

        private int NRG(Vector3 v)
        {
            return (int)(Math.Abs(v.X) + Math.Abs(v.Y) + Math.Abs(v.Z));
        }

        public override object Task2()
        {
            Moon[] moons = new Moon[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                string[] parts = input[i].Substring(1, input[i].Length - 2).Split(", ");
                moons[i] = new Moon(int.Parse(parts[0].Split('=')[1]),
                                    int.Parse(parts[1].Split('=')[1]),
                                    int.Parse(parts[2].Split('=')[1]));
            }

            long[] intervals = new long[3];
            int found = 0;
            for (int step = 1; found < 3; step++)
            {
                SimulateStep(moons);

                bool[] stopped = new bool[] { true, true, true };

                for (int i = 0; i < moons.Length; i++)
                {
                    stopped[0] &= moons[i].Velocity.X == 0;
                    stopped[1] &= moons[i].Velocity.Y == 0;
                    stopped[2] &= moons[i].Velocity.Z == 0;
                }


                for (int i = 0; i < intervals.Length; i++)
                {
                    if (intervals[i] == 0 && stopped[i])
                    {
                        intervals[i] = step * 2;
                        found++;
                    }
                }
            }
            return LCM(intervals);

        }

        private long GCD(long a, long b)
        {
            while (b != 0)
            {
                long tmp = b;
                b = a % b;
                a = tmp;
            }
            return Math.Abs(a);
        }

        private long LCM(long[] nums)
        {
            long answer = (nums[0] * nums[1]) / GCD(nums[0], nums[1]);

            for (int i = 2; i < nums.Length; i++)
            {
                answer = (answer * nums[i]) / GCD(answer, nums[i]);
            }
            return answer;
        }
    }
}