
public class Solution2 {

    public static void Mainw(){
        //[5,4,6,null,null,3,7]
        var s = new Solution();
        var n1 = new TreeNode(5);
        var n2 = new TreeNode(4);
        var n3 = new TreeNode(6);
        var n4 = new TreeNode(3);
        var n5 = new TreeNode(7);
        n1.left = n2;
        n1.right = n3;
        n3.left = n4;
        n3.right = n5;
        Console.WriteLine(s.IsValidBST(n1));
    }

    // ------------ Binary Tree ------------
    //there are inorder and preorder traversals of same binary tree, construct the binary tree
    //time complexity: O(n), memory complexity: O(n) 
    public class BT1 {
    private int[] preorder_;
    private Dictionary<int, int> inorderIndecies;

    public TreeNode BuildTree(int[] preorder, int[] inorder) 
    {
        var n = preorder.Length;
        preorder_ = preorder;

        inorderIndecies = new Dictionary<int, int>();
        for (int i = 0; i < n; i++)
            inorderIndecies[inorder[i]] = i;

        return BuildTree(0, n-1, 0, n-1);
    }

    public TreeNode BuildTree(int preStart, int preEnd, int inStart, int inEnd) 
    {
        if (preStart > preEnd || inStart > inEnd)
            return null;

        var root = new TreeNode(preorder_[preStart]);

        var leftSize = inorderIndecies[root.val] - inStart;

        root.left = BuildTree(preStart + 1, preStart + leftSize, inStart, inStart + leftSize);
        root.right = BuildTree(preStart + leftSize + 1, preEnd, inStart + leftSize + 1, inEnd);

        return root;
    }
}

    // ------------ Binary Search ------------
    public int BinarySearch(int[] nums, int target) {
        int rI = nums.Length - 1;
        int lI = 0;

        while(lI <= rI)
        {
            int middle = (int)((rI + lI)/2);
            if(nums[middle] < target)
                lI = middle + 1;
            else if(nums[middle] != target)
                rI = middle - 1;
            else 
             return middle; 
        }
        return -1;
    }

    // ------------ Construct Binary Tree from Preorder and Inorder Traversal ------------
    // simple O(n^2)
    public TreeNode BuildTree(int[] preorder, int[] inorder) 
    {
        if(preorder.Length == 0 || inorder.Length == 0)
            return null;

        int inorderIndex = Array.IndexOf(inorder, preorder[0]);

        return new TreeNode(preorder[0],
                            BuildTree(preorder[1..(inorderIndex+1)], inorder[0..inorderIndex]),
                            BuildTree(preorder[(inorderIndex+1)..], inorder[(inorderIndex+1)..]));
    }
    // --- faster O(n) ---
    public class Solution3 {
        private Dictionary<int, int> _inorderIndexes = new Dictionary<int, int>();
        private int[] _preorder;

        public TreeNode BuildTree(int[] preorder, int[] inorder) 
        {
            if(preorder.Length == 0 || inorder.Length == 0)
                return null;

            _preorder = preorder;
            for(int i = 0; i < inorder.Length; i++)
                _inorderIndexes[inorder[i]] = i;

            return BuildSubTree(0, preorder.Length-1, 0, preorder.Length-1);
        }

        private TreeNode BuildSubTree(int pLeft, int pRight, int iLeft, int iRight)
        {
            if(pLeft > pRight || iLeft > iRight)
                return null;

            var size = _inorderIndexes[_preorder[pLeft]] - iLeft;

            return new TreeNode(_preorder[pLeft],
                                BuildSubTree(pLeft+1, pLeft + size, iLeft, iLeft + size),
                                BuildSubTree(pLeft+size+1, pRight, iLeft+size+1, iRight));
        }
    }
    // ------------ Depth First Search DFS ------------
    public IList<int> InorderTraversal(TreeNode root) {
        if(root == null)
            return new List<int>();

        var list = new List<int>();
        list.AddRange(InorderTraversal(root.left));
        list.Add(root.val);
        list.AddRange(InorderTraversal(root.right));
        return list;
    }
    public class TreeNode {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int val=0, TreeNode left=null, TreeNode right=null) {
            this.val = val;
            this.left = left;
            this.right = right;
        }
    }

    // --- 2 ---
    //     public long? GetMinValue(TreeNode root){
    //     if(root == null)
    //         return null;

    //     long left = GetMinValue(root.left) ?? root.val;
    //     long right = GetMinValue(root.right) ?? root.val;

    //     if(root.val <= left)
    //         return Math.Min(root.val, right);
    //     else
    //         return Math.Min(left, right);
    // }
    // public long? GetMaxValue(TreeNode root){
    //     if(root == null)
    //         return null;

    //     long left = GetMaxValue(root.left) ?? root.val;
    //     long right = GetMaxValue(root.right) ?? root.val;

    //     if(root.val <= left)
    //         return Math.Max(left, right);
    //     else
    //         return Math.Max(root.val, right);
    // }

    // public bool IsValidBST(TreeNode root) {
    //     if(root == null)
    //         return true;

    //     long val = GetMinValue(root.right) ?? (root.val + 1);
    //     var b1 = root.val > (GetMaxValue(root.left) ??  (root.val - 1));
    //     var b2 = root.val < (GetMinValue(root.right) ?? (root.val + 1));
    //     var b3 = IsValidBST(root.right);
    //     var b4 = IsValidBST(root.left);
    //     if(root.val > (GetMaxValue(root.left) ??  (root.val - 1))
    //         && root.val < (GetMinValue(root.right) ?? (root.val + 1))
    //         && IsValidBST(root.right)
    //         && IsValidBST(root.left))
    //         return true;

    //     return false;
    // }
 // --- 2.1 ---
    // public int GetMinValue(TreeNode root){
    //     int result = root.val;

    //     if(root.left != null)
    //         result = Math.Min(result, GetMinValue(root.left));
        

    //     if(root.right != null)
    //         result = Math.Min(result, GetMinValue(root.right));
        
    //     return result;
    // }
    // public int GetMaxValue(TreeNode root){
    //     int result = root.val;

    //     if(root.left != null)
    //         result = Math.Max(result, GetMaxValue(root.left));
        
    //     if(root.right != null)
    //         result = Math.Max(result, GetMaxValue(root.right));
        
    //     return result;
    // }

    // public bool IsValidBST(TreeNode root) {
    //     if(root == null)
    //         return true;

    //     bool cond1;
    //     if(root.left != null)
    //         cond1= root.val > GetMaxValue(root.left);
    //     else 
    //         cond1 = true;

    //     bool cond2;
    //     if(root.right != null)
    //         cond2 = root.val < GetMinValue(root.right);
    //     else 
    //         cond2 = true;

    //     if(cond1 && cond2
    //         && IsValidBST(root.right)
    //         && IsValidBST(root.left))
    //         return true;

    //     return false;
    // }
    // --2.2 --
    public bool IsValidBST(TreeNode root) {
        return Evaluate(root, long.MinValue, long.MaxValue);
    }
    private bool Evaluate(TreeNode node, long min, long max)
    {
        if (node == null)
            return true;

        return (
            node.val > min &&
            node.val < max &&
            Evaluate(node.left, min, node.val) &&
            Evaluate(node.right, node.val, max)
        );   
    }

    //------------ Recover Binary Search Tree ------------

    public class Solution4 {
        private TreeNode _first;
        private TreeNode _second;
        private TreeNode _previous;
        
        public void RecoverTree(TreeNode root) {
            findBadNodes(root);

            if(_first != null)
            {
                int temp = _first.val;
                _first.val = _second.val;
                _second.val = temp;
            }
        }

        private void findBadNodes(TreeNode node)
        {
            if(node == null)
                return;
            findBadNodes(node.left);

            List<int> ints = new List<int>(){1,2,3,4,5,6,7,8,9,10}; 
            bool b = ints.Count > 0;


            if(_previous?.val >= node.val)
            {
                if(_first == null)
                    _first = _previous;
                _second = node;
            }
            _previous = node;

            findBadNodes(node.right);
        }
    }

    //------------ 173. Binary Search Tree Iterator ------------
    // 1 ms (100%) Next and HasNext: O(1); 63 MB (5.7%) memory: O(n)
    public class BSTIterator2 {
        private List<int> _inorder = new List<int>();
        private int _currentIdx = 0;
        public BSTIterator2(TreeNode root) {
            FillInorderList(root);
        }
        private void FillInorderList(TreeNode node)
        {
            if(node == null)
                return;

            FillInorderList(node.left);
            _inorder.Add(node.val);
            FillInorderList(node.right);
        }
        public int Next() {
            return _inorder[_currentIdx++];
        }
        public bool HasNext() {
            return _currentIdx + 1 <= _inorder.Count;
        }
    }
    //version with Queue is slower because it additionally removes elements
    // 2 ms (23.36%) 63 MB (5.7%) 
    public class BSTIterator {
    private Queue<int> _queue = new Queue<int>();
    public BSTIterator(TreeNode root) {
        FillInorderList(root);
    }
    private void FillInorderList(TreeNode node)
    {
        if(node == null)
            return;

        FillInorderList(node.left);
        _queue.Enqueue(node.val);
        FillInorderList(node.right);
    }
    public int Next() {
        return _queue.Dequeue();
    }
    public bool HasNext() {
        return _queue.Count > 0;
    }
}



}