using System;

namespace Day8
{
    /// <summary>
    /// Represents a machine instruction
    /// </summary>
    public class Instruction : ICloneable
    {
        /// <summary>
        /// Gets or sets a value indicating the operator to execute
        /// </summary>
        public string Op
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the operator value
        /// </summary>
        public int Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the execution count of the instruction
        /// </summary>
        public int ExecutionCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets a value indicating the non-interpreted raw value of the instruction
        /// </summary>
        /// <remarks>
        /// If the instruction is overridden outside of initialization, this will reflect incorrectly. <see cref="ToString"/> will always show the current value.
        /// </remarks>
        public string RawOriginalValue
        {
            get;
        }

        public Instruction(string s)
        {
            this.RawOriginalValue = s;
            string[] p = s.Split(' ');
            this.Op = p[0];
            this.Value = int.Parse(p[1]);
        }

        public Instruction()
        {
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"op={this.Op} v={this.Value}";
        }

        /// <inheritdoc />
        public object Clone()
        {
            Instruction i = new Instruction { Op = this.Op, Value = this.Value };
            return i;
        }
    }
}