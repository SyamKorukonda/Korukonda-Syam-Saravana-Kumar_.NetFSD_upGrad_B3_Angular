// -------- Button Click --------
function f1()
{
    if(navigator.geolocation)
    {
        navigator.geolocation.getCurrentPosition(f2, f3);
    }
    else
    {
        document.getElementById("msg").innerHTML="Geolocation not supported";
    }
}


// -------- Success (Location Found) --------
function f2(pos)
{
    var lat = pos.coords.latitude;
    var lon = pos.coords.longitude;

    // Display current location
    document.getElementById("lat").innerHTML = "Latitude : " + lat;
    document.getElementById("lon").innerHTML = "Longitude : " + lon;
    document.getElementById("msg").innerHTML = "Location Found ✔";

    // Create object
    var locationObj = {
        latitude: lat,
        longitude: lon
    };

    // Get previous data
    var history = JSON.parse(localStorage.getItem("locations")) || [];

    // Add new location
    history.unshift(locationObj);

    // Keep only 5 entries
    if(history.length > 5)
    {
        history.pop();
    }

    // Save in localStorage
    localStorage.setItem("locations", JSON.stringify(history));

    // Update list
    showHistory();
}


// -------- Error Handling --------
function f3(error)
{
    if(error.code == 1)
        document.getElementById("msg").innerHTML="Permission Denied";
    else if(error.code == 2)
        document.getElementById("msg").innerHTML="Location Unavailable";
    else if(error.code == 3)
        document.getElementById("msg").innerHTML="Request Timeout";
}


// -------- Display History --------
function showHistory()
{
    var list = document.getElementById("history");
    list.innerHTML="";

    var history = JSON.parse(localStorage.getItem("locations")) || [];

    for(var i=0; i<history.length; i++)
    {
        var li = document.createElement("li");
        li.innerHTML = "Lat: " + history[i].latitude +
                       " | Lon: " + history[i].longitude;

        list.appendChild(li);
    }
}

// run automatically when page loads
showHistory();