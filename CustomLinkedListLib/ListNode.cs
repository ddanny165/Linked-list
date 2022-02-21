namespace CustomLinkedListLib
{
    public class ListNode<T>
    {
        public ListNode<T> NextNode { get; set; }

        public T Value { get; set; }

        public ListNode<T> PreviousNode { get; set; }

        public ListNode()
        {
            this.Value = default(T);
            this.PreviousNode = null;
            this.NextNode = null;
        }

        public ListNode(T Value)
        {
            this.Value = Value;
            this.PreviousNode = null;
            this.NextNode = null;
        }

        public ListNode(T Value, ListNode<T> PreviousNode)
        {
            this.Value = Value;
            this.PreviousNode = PreviousNode;
            this.NextNode = null;
        }

        public override string ToString()
        {
            return $"Current node value: {Value ?? default(T)}";
        }
    }
}
