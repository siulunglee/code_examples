public class BinarySearchTree
{
    static void Main(string[] args)
    {
        BinarySearchTree bst = new BinarySearchTree();
        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(15);
        bst.Insert(3);
        bst.Insert(7);
        bst.Insert(12);
        bst.Insert(18);

        Console.WriteLine("Binary Search Tree created and values inserted.");

        Console.WriteLine("== Display List: ==");
        bst.traverseInOrder();

        Node minNode = bst.FindMin(bst.root);
        Console.WriteLine($"Minimum value in the BST: {minNode.Value}");

        Node maxNode = bst.FindMax(bst.root);
        Console.WriteLine($"Maximum value in the BST: {maxNode.Value}");

        int searchValue0 = 10;
        Node searchResult0 = bst.Search(searchValue0);
        Console.WriteLine(searchResult0 != null ? $"Found node with value: {searchResult0.Value}" : $"Node with {searchValue0} not found.");

        bst.Delete(10);
        Console.WriteLine("Deleted node with value 10.");

        Console.WriteLine("== Display List: ==");
        bst.traverseInOrder();

        int searchValue1 = 7;
        int searchValue2 = 44;
        int searchValue3 = 10;
        Node searchResult1 = bst.Search(searchValue1);
        Node searchResult2 = bst.Search(searchValue2);
        Console.WriteLine(searchResult1 != null ? $"Found node with value: {searchResult1.Value}" : $"Node with {searchValue1} not found.");
        Console.WriteLine(searchResult2 != null ? $"Found node with value 2: {searchResult2.Value}" : $"Node with {searchValue2} not found.");
        Node searchResult3 = bst.Search(searchValue3);
        Console.WriteLine(searchResult3 != null ? $"Found node with value 3: {searchResult3.Value}" : $"Node with {searchValue3} not found.");

        // Console.WriteLine("Max Value", bst.FindMax(bst.root));
        // Console.WriteLine("Min Value", bst.FindMin(bst.root));

    }

    public class Node
    {
        public int Value;
        public Node Left;
        public Node Right;

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    private Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    public void Insert(int value)
    {
        root = InsertRec(root, value);
    }

    private Node InsertRec(Node root, int value)
    {
        if (root == null)
        {
            return new Node(value);
        }

        if (value < root.Value)
        {
            root.Left = InsertRec(root.Left, value);
        }
        else if (value > root.Value)
        {
            root.Right = InsertRec(root.Right, value);
        }

        return root;
    }

    public Node FindMin(Node node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }
        return node;
    }


    public Node FindMax(Node node)
    {
        while (node.Right != null)
        {
            node = node.Right;
        }
        return node;
    }


    public Node Delete(int value)
    {
        return DeleteRec(root, value);
    }

    private Node DeleteRec(Node root, int value)
    {
        if (root == null)
        {
            return root;
        }

        if (value < root.Value)
        {
            root.Left = DeleteRec(root.Left, value);
        }
        else if (value > root.Value)
        {
            root.Right = DeleteRec(root.Right, value);
        }
        else
        {
            // Node with only one child or no child
            if (root.Left == null)
            {
                return root.Right;
            }
            else if (root.Right == null)
            {
                return root.Left;
            }

            // Node with two children: Get the in-order successor (smallest in the right subtree)
            Node temp = FindMin(root.Right);
            root.Value = temp.Value; // Copy the in-order successor's value to this node
            root.Right = DeleteRec(root.Right, temp.Value); // Delete the inorder successor
        }
        return root;
    }


    public Node Search(int value)
    {
        return SearchRec(root, value);
    }

    private Node SearchRec(Node root, int value)
    {
        if (root == null || root.Value == value)
        {
            return root;
        }

        if (value < root.Value)
        {
            return SearchRec(root.Left, value);
        }
        else
        {
            return SearchRec(root.Right, value);
        }
    }
    
    public Node traverseInOrder()
    {
        return traverseInOrderRec(root);
    }

    public Node traverseInOrderRec(Node node)
    {
        if (node == null)
        {
            return null;
        }

        traverseInOrderRec(node.Left);
        Console.WriteLine(node.Value);
        traverseInOrderRec(node.Right);

        return node;
    }

}