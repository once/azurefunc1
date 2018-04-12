using System.Net;

const double revenuePerkW = 0.12; 
const double technicianCost = 250; 
const double turbineCost = 100;

public static async Task<HttpResponseMessage> Run(HttpRequestMessage req, TraceWriter log)
{
    //Get request body
    dynamic data = await req.Content.ReadAsAsync<object>();
    int hours = data.hours;
    int capacity = data.capacity;

    //Formulas to calculate revenue and cost
    double revenueOpportunity = capacity * revenuePerkW * 24;  
    double costToFix = (hours * technicianCost) +  turbineCost;
    string repairTurbine;

    if (revenueOpportunity > costToFix){
        repairTurbine = "Yes, required";
    }
    else {
        repairTurbine = "No, not required";
    }

    return req.CreateResponse(HttpStatusCode.OK, new{
        message = repairTurbine,
        revenueOpportunity = "$"+ revenueOpportunity,
        costToFix = "$"+ costToFix         
    }); 
}
