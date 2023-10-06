using System;


namespace Practice2;


public class Practice2{

public class Node{
   public Node Right {get; set;}
   public Node Left {get; set;}
   public Node Parent {get; set;}
   
}
public Node FindLeft(Node input){
   if(input.Left == null)
      return input;

   return FindLeft(input.Left);
}
public Node FindRight(Node input){
   if(input.Parent.Left == input)
      return input;

   return FindRight(input.Parent);
}
public Node FindInOrderSuccesor(Node input){
   //down
   if(input.Right != null)
      return FindLeft(input.Right);

   //up

   return FindRight(input.Parent);

}
public static void Mainc(string[] args){
   
   

}
}
