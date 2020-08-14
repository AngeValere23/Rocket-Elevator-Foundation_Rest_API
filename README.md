Rocket // Odyssey Week 8
Rocket Elevators Status REST API
How to answer the 9 questions in Postman :
1- To Get a specified Battery current status or all informations, do :
    GET https://localhost:5001/api/batteries/5 [5 = specified battery ID]
    SEND

2- To Modify the status of a specified Battery, do :
    PUT https://localhost:5001/api/batteries/5 or all [5 = specified battery ID]
    SEND
    Select:  Body
                Raw
                JSON application
    In the text field, enter:

{
	"status": "Active" _or_ "Inactive"  _to change the status_ 
} 
    SEND
    You can verify if the change succeeded by doing a new GET on that specified Battery.

3- To Get a specified Column current status or all informations, do :
    GET https://localhost:5001/api/columns/5 or all [5 = specified column ID]
    SEND

4- To Modify the status of a specified Column, do :
    PUT https://localhost:5001/api/columns/5 [5 = specified column ID]
    SEND
    Select:  Body
                Raw
                JSON application
    In the text field, enter:

{ 
	"status": "Active" _or_ "Inactive"  _to change the status_ 
} 
    SEND
    You can verify if the change succeeded by doing a new GET on that specified Column.

5- To Get a specified Elevator current status or all informations, do :
    GET https://localhost:5001/api/elevators/1 or all [1 = specified elevator ID]
    SEND

6- To Modify the status of a specified Elevator, do :
    PUT https://rocketapi.azure-api.net/api/elevators/1 [1 = specified elevator ID]
    SEND
    An error will appear in the field, that's ok.
    Select:  Body
                Raw
                JSON application
    In the text field, enter:

{ 
	"status": "Active" _or_ "Inactive"  _to change the status_ 
} 
    SEND
    You can verify if the change succeeded by doing a new GET on that specified Elevator.

    Or you can also : PUT https://localhost:5001/api/elevators/1/inactive OR active, that will change the status too.

7- To Get the elevators list that are not in operation at the moment, do :
    GET https://localhost:5001/api/elevators/notinoperation
    SEND

8- To Get a buildings list that have at least one Battery, one Column or one Elevator needing an intervention, do :
    GET https://localhost:5001/api/buildings/
    SEND

9- To Get a Leads list that are not our clients yet since the last 30 days, do :
    GET https://localhost:5001/api/leads/
    SEND


# Rocket-Elevator-Foundation_Rest_API
