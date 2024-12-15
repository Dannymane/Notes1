
public class Solution {

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
    // ------------ Binary Search ------------
    public int BinarySearch(int[] nums, int target) {
        int rightI = nums.Length - 1;
        int leftI = 0;

        while(leftI <= rightI){
            var middle = (int)((rightI + leftI)/2);

            if(nums[middle] == target)
                return middle;
            else if(nums[middle] < target)
                leftI = middle + 1;
            else
                rightI = middle - 1;
        }
        return -1;
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
        {
            return true;
        }

        return (
            node.val > min &&
            node.val < max &&
            Evaluate(node.left, min, node.val) &&
            Evaluate(node.right, node.val, max)
        );
    }
}