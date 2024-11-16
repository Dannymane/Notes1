
public class Solution {
    public int BinarySearch(int[] nums, int target) {
        int rightI = nums.Length - 1;
        int leftI = 0;

        while(leftI <= rightI){
            var middle = (int)((rightI + leftI)/2);

            if(nums[middle] == target)
                return middle;
            else if(nums[middle] <= target)
                leftI = middle + 1;
            else
                rightI = middle - 1;
        }
        return -1;
    }

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
}