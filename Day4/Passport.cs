using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Day4
{
    public class Passport
    {
        private static readonly List<string> ValidEyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

        private readonly int birthYear;
        private readonly int issueYear;
        private readonly int expiryYear;
        private readonly int height;
        private readonly string hairColor;
        private readonly string eyeColor;
        private readonly string passportId;
        private readonly int countryId;
        private readonly bool strict;
        private readonly string heightUnit;

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
        public Passport(IList<string> rows, ref int index, bool strict)
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
                            string h = parts[1];
                            if (h.EndsWith("in") == false && h.EndsWith("cm") == false)
                            {
                                this.height = 999;
                            }
                            else
                            {
                                this.height = GetInt(h.Substring(0, h.Length - 2), false);
                                this.heightUnit = h.Substring(h.Length - 2, 2);
                            }
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
                            this.passportId = parts[1];
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
                return this.IsValidPassport();
            }
        }

        private bool IsValidPassport()
        {
            bool basicValidation =
                    string.IsNullOrEmpty(this.eyeColor) == false &&
                    string.IsNullOrEmpty(this.hairColor) == false &&
                    string.IsNullOrEmpty(this.passportId) == false &&
                    this.height > 0 &&
                    this.birthYear > 0 &&
                    this.expiryYear > 0 &&
                    this.issueYear > 0;

            if (this.strict == false)
            {
                return basicValidation;
            }
            else
            {
                bool isValid =
                    basicValidation == true &&
                    (this.birthYear >= 1920 && this.birthYear <= 2002) &&
                    (this.issueYear >= 2010 && this.issueYear <= 2020) &&
                    (this.expiryYear >= 2020 && this.expiryYear <= 2030) &&
                    (this.heightUnit == "cm" || this.heightUnit == "in") &&
                    ((this.heightUnit == "cm" && this.height >= 150 && this.height <= 193) ||
                    (this.heightUnit == "in" && this.height >= 59 && this.height <= 76)) &&
                    string.IsNullOrEmpty(this.hairColor) == false &&
                    ValidEyeColors.Contains(this.eyeColor) &&
                    Regex.IsMatch(this.hairColor, "^#[0-9a-f]{6,6}$") &&
                    Regex.IsMatch(this.passportId, @"^[\d+]{9,9}$");
                return isValid;
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