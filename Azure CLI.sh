#check any value in bash - use echo
echo $RANDOM
#returns a random number between 0 and 32767

#login in Azure CLI
az login --use-device-code #--use-device-code redirects to browser and enables required multifactor authorization 

#get the list of all resource groups
az group list 
    --query "[].{nameForVariableInOutput:name, Location:location}" 
    --output table 
# "[].{}" is Na JMESPath query that selects all elements in the array [] and creates a new object {} with the specified properties.
# just az group list

#get certail resource group info
az group show --name <group_name>

#get all group resources
az resource list --resource-group <group_name>

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

#Create and Deploy the web app 
az webapp up 
    -g $resourceGroup #g is short for --resource-group
    -n $appName #use appName that you defined when created App Service Plan
    --html  #n is short for --name

#Deploy new build to already existing app
az webapp deploy 
--name dy-school-register
--resource-group main-rg 
--src-path releaseVersion.zip #PowerShell must be at directory with .zip or provide full path
--type zip

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