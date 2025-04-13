#check any value in bash - use echo
echo $RANDOM
#returns a random number between 0 and 32767

#get the list of all resource groups
az group list \
    --query "[].{nameForVariableInOutput:name, Location:location}" \
    --output table 
# "[].{}" is a JMESPath query that selects all elements in the array [] and creates a new object {} with the specified properties.

#get the list of all web apps
az webapp list \
    --query "[].{nameForVariableInOutput:name, Location:location}" \
    --output tsv #instead of --output may be just -o


#Get the list of all web apps in a specific resource group - add
    --resource-group <group_name> \

#clone git repo to current (bash) directory
git clone https://github.com/Azure-Samples/html-docs-hello-world.git

#create bash variables
resourceGroup=$(az group list --query "[].{id:name}" -o tsv)
appName=az204app$RANDOM
#$(...) is command substitution in Bash — it runs the command inside and stores its output in the variable.

#Display the value of the variable
echo $resourceGroup

#Deploy the web app
az webapp up 
    -g $resourceGroup #g is short for --resource-group
    -n $appName --html  #n is short for --name


#the outbound IP addresses currently used by app (equal to all used outbound IP used by plan)
az webapp show \
    --resource-group <group_name> \
    --name <app_name> \ 
    --query outboundIpAddresses \
    --output tsv #tsv fomrat - tab separated values 

#Check all possible outbound Ips of plan (enter any app_name from plan)
az webapp show \
    --resource-group <group_name> \ 
    --name <app_name> \ 
    --query possibleOutboundIpAddresses \
    --output tsv