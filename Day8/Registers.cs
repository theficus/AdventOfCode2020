namespace Day8
{
    public class Registers
    {
        public int Accumulator
        {
            get;
            set;
        }

        public int InstructionPtr
        {
            get;
            set;
        }

        public override string ToString()
        {
            return $"ip={this.InstructionPtr} ac={this.Accumulator}";
        }
    }
}