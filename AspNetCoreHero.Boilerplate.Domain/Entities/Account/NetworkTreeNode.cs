public class NetworkTreeNode : AuditableEntity, IAggregateRoot
{
    public NetworkPosition Type { get; set; } = NetworkPosition.Right;
    public DefaultIdType? ParentNodeId { get; set; }
    public NetworkTreeNode? ParentNode { get; set; }
    public List<NetworkTreeNode>? ChildNodes { get; set; }
}
