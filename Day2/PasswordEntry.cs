using System;
using System.Text.RegularExpressions;

namespace Day2
{
    public class PasswordEntry
    {
        // Goal 2
        private readonly int position1;

        // Goal 2
        private readonly int position2;

        // Goal 1
        private readonly int minOccurrence;

        // Goal 1
        private readonly int maxOccurrence;

        private readonly char requiredChar;

        private readonly string password;

        public PasswordEntry(string entry)
        {
            // Format: 1-10 w: wwwwwcwwwrpnwzwxww
            MatchCollection parts = Regex.Matches(entry, @"^(\d+)\-(\d+) (\w)\: (.*)$");
            GroupCollection groups = parts[0].Groups;

            this.position1 = this.minOccurrence = int.Parse(groups[1].Value);
            this.position2 = this.maxOccurrence = int.Parse(groups[2].Value);
            this.requiredChar = groups[3].Value[0];

            this.password = groups[4].Value;
        }

        /// <summary>
        /// Is the password valid per policy (Goal 1)
        /// </summary>
        /// <remarks>
        /// Each line gives the password policy and then the password. The password policy indicates the lowest and highest number of times a given letter must appear for the password
        /// to be valid. For example, 1-3 a means that the password must contain a at least 1 time and at most 3 times.
        /// </remarks>
        public bool IsValidGoal1
        {
            get
            {
                int occurs = 0;
                for (int i = 0; i < this.password.Length; i++)
                {
                    if (this.password[i] == this.requiredChar)
                    {
                        occurs++;
                        if (occurs > this.maxOccurrence)
                        {
                            return false;
                        }
                    }
                }

                return occurs >= this.minOccurrence;
            }
        }

        /// <summary>
        /// Is the password valid per policy (Goal 2)
        /// </summary>
        /// <remarks>
        /// Each policy actually describes two positions in the password, where 1 means the first character, 2 means the second character, and so on.
        /// (Be careful; Toboggan Corporate Policies have no concept of "index zero"!)
        ///
        /// Exactly one of these positions must contain the given letter.
        ///
        /// Other occurrences of the letter are irrelevant for the purposes of policy enforcement.
        /// </remarks>
        public bool IsValidGoal2
        {
            get
            {
                char p1 = this.password[this.position1 - 1];
                char p2 = this.password[this.position2 - 1];

                return p1 != p2 && (p1 == this.requiredChar || p2 == this.requiredChar);
            }
        }
    }
}
