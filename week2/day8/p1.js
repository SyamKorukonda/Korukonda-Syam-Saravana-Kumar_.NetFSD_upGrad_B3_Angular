const res=document.getElementById("result");

async function getweather(){
    try{
        let city=document.getElementById("city").value;
        let response= await fetch(`https://geocoding-api.open-meteo.com/v1/search?name=${city}&count=1`);
        let responsedata= await response.json();

        if(!responsedata.results)
            throw "city not found";
        let lat=responsedata.results[0].latitude;
        let log=responsedata.results[0].longitude;
        
        let weather=await fetch(`https://api.open-meteo.com/v1/forecast?latitude=${lat}&longitude=${log}&current_weather=true`);
        let weatherdata= await weather.json();

        let w=weatherdata.current_weather;

        result.innerHTML = `
            <h3>${city}</h3>
            <p>Temperature: ${w.temperature} °C</p>
            <p>Wind Speed: ${w.windspeed} km/h</p>
        `;

    }
    catch(err){
        result.innerHTML = `<p style="color:red;">${err}</p>`;
    }
}