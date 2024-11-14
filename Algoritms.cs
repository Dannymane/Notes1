
public class Solution {
    public int BinarySearch(int[] nums, int target) {
        int rightI = nums.Length - 1;
        int leftI = 0;

        while(leftI <= rightI){
            var middle = (rightI + leftI)/2;

            if(nums[middle] == target)
                return middle;
            else if(nums[middle] <= target)
                leftI = middle + 1;
            else
                rightI = middle - 1;
        }
        return -1;
    }
}