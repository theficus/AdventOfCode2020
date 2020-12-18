namespace Day8
{
    public class Instruction
    {
        public string Op
        {
            get;
            set;
        }

        public int Value
        {
            get;
            set;
        }

        public int ExecutionCount
        {
            get;
            set;
        }

        public Instruction(string s)
        {
            string[] p = s.Split(' ');
            this.Op = p[0];
            this.Value = int.Parse(p[1]);
        }

        public Instruction()
        {
        }

        public override string ToString()
        {
            return $"op={this.Op} v={this.Value}";
        }
    }
}
