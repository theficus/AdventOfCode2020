using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Day4
{
    public class Passport
    {
        private static readonly List<string> ValidEyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        private readonly int birthYear;
        private readonly int issueYear;
        private readonly int expiryYear;
        private readonly string height;
        private readonly string hairColor;
        private readonly string eyeColor;
        private readonly int passportId;
        private readonly int countryId;
        private readonly bool strict;

        /*
         * byr (Birth Year)
         * iyr (Issue Year)
         * eyr (Expiration Year)
         * hgt (Height)
         * hcl (Hair Color)
         * ecl (Eye Color)
         * pid (Passport ID)
         * cid (Country ID)
         */
        public Passport(IList<string> rows, ref int index, bool strict = false)
        {
            this.strict = strict;
            for (; index < rows.Count; index++)
            {
                if (string.IsNullOrWhiteSpace(rows[index]) == true)
                {
                    index++;
                    return;
                }

                foreach (string s in rows[index].Split(' '))
                {
                    string[] parts = s.Split(':');
                    switch (parts[0])
                    {
                        case "byr":
                            this.birthYear = GetInt(parts[1], strict);
                            break;
                        case "iyr":
                            this.issueYear = GetInt(parts[1], strict);
                            break;
                        case "eyr":
                            this.expiryYear = GetInt(parts[1], strict);
                            break;
                        case "hgt":
                            this.height = parts[1];
                            break;
                        case "hcl":
                            this.hairColor = parts[1];
                            break;
                        case "ecl":
                            this.eyeColor = parts[1];
                            break;
                        case "cid":
                            this.countryId = GetInt(parts[1], strict);
                            break;
                        case "pid":
                            this.passportId = GetInt(parts[1], strict);
                            break;
                        default:
                            throw new InvalidOperationException($"Unrecognized type: {parts[0]}");
                    }
                }
            }
        }

        public bool IsValid
        {
            get
            {
                bool basicValidation =
                    string.IsNullOrEmpty(this.eyeColor) == false &&
                    string.IsNullOrEmpty(this.hairColor) == false &&
                    this.passportId > 0 &&
                    string.IsNullOrEmpty(this.height) == false &&
                    this.birthYear > 0 &&
                    this.expiryYear > 0 &&
                    this.issueYear > 0;

                if (this.strict == false)
                {
                    return basicValidation;
                }
                else
                {
                    return
                        ValidEyeColors.Contains(this.eyeColor) &&
                        this.birthYear >= 1920 &&
                        this.birthYear <= 2002 &&
                        this.issueYear >= 2010 &&
                        this.issueYear <= 2020 &&
                        this.expiryYear >= 2020 &&
                        this.expiryYear <= 2030 &&
                        Regex.IsMatch(this.hairColor, "^#(0-9a-f){0,6}$") &&
                        Regex.IsMatch($"{0:9}", @"^(\d+){0,9}$");
                }
            }
        }

        private int GetInt(string v, bool strict)
        {
            int i;
            if (int.TryParse(v, out i) == false &&
                strict == false)
            {
                return 999;
            }

            return i;
        }
    }
}
