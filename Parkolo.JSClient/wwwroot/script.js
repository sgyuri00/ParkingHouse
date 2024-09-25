//variables to store data and chech SignalR status
let cars = [];
let sum = [];
let over = [];
let paid = [];
let connection = null;

//get data for first run and start SignalR
getdata();
setupSignalR();

//setup SignalR
function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:45793/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("AutoCreated", (user, message) => {
        getdata();
    });
    connection.on("AutoDeleted", (user, message) => {
        getdata();
    });
    connection.on("AutoUpdated", (user, message) => {
        getdata();
    });

    connection.onclose
        (async () => {
            await start();
        });

    start();

}
//start SignalR
async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};
//get data after any change
async function getdata() {
    await fetch('http://localhost:45793/Auto')
        .then(x => x.json())
        .then(y => {
            cars = y;
            display();
        });
    await fetch('http://localhost:45793/Over8Hours')
        .then(x => x.json())
        .then(y => {
            over = y;
            displayover();
        });
    await fetch('http://localhost:45793/PaidMoreThan2000')
        .then(x => x.json())
        .then(y => {
            paid = y;
            displaymore();
        });
    await fetch('http://localhost:45793/SumCost')
        .then(x => x.json())
        .then(y => {
            sum = y;
            displaysum();
        });
}
//display main table data
function display() {
    document.getElementById('resultarea').innerHTML = "";

    cars.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.rendszam + "</td><td>" + t.gyartasiEv + "</td><td>" + t.marka + "</td><td>" + t.uzemanyag +
        "</td><td>" +
        `<button type="button" onclick="remove('${t.rendszam}')">Delete</button>` +
        `<button type="button" onclick="showupdate('${t.rendszam}',${t.gyartasiEv},'${t.marka}','${t.uzemanyag}')">Update</button>`
            + "</td></tr>";
            console.log(t.rendszam)
    });
}
//delete main table data
function remove(id) {
    fetch('http://localhost:45793/Auto/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdatafirst();
        })
        .catch((error) => { console.error('Error:', error); });
}
//delete main table data
function showupdate(id, yearup, brandup, fuelup) {
    document.getElementById('rendszamtoup').value = cars.find(t => t['rendszam'] == id)['rendszam'];
    document.getElementById('uzemanyagtoupdate').value = cars.find(t => t['rendszam'] == id)['uzemanyag'];
    document.getElementById('gyartasiEvtoup').value = cars.find(t => t['rendszam'] == id)['gyartasiEv'];
    document.getElementById('markatoup').value = cars.find(t => t['rendszam'] == id)['marka'];

    document.getElementById('updateformdiv').style.display = 'flex';
}
//update main table fuel
function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let platenum = document.getElementById('rendszamtoup').value;
    let year = document.getElementById('gyartasiEvtoup').value;
    let brand = document.getElementById('markatoup').value;
    let fuel = document.getElementById('uzemanyagtoupdate').value;
    fetch('http://localhost:45793/Auto', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { rendszam: platenum, marka: brand, gyartasiEv: year, uzemanyag: fuel }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdatafirst();
        })
        .catch((error) => {
            console.error('Error:', error);
        });

}
//create main table data
function create() {
    let platenum = document.getElementById('rendszam').value;
    let year = document.getElementById('gyartasiEv').value;
    let brand = document.getElementById('marka').value;
    let fuel = document.getElementById('uzemanyag').value;
    fetch('http://localhost:45793/Auto', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { rendszam: platenum, marka: brand, gyartasiEv: year, uzemanyag: fuel }),
    })
        .then(response => response)
        .then(data =>
        {
            console.log('Success:', data);
            getdatafirst();
        })
        .catch((error) => {
            console.error('Error:', error);
        });
    
}
//display noncrud
function displaysum() {
    document.getElementById('sumarea').innerHTML = "";

    sum.forEach(t => {
        document.getElementById('sumarea').innerHTML +=
            "<tr><td>" + t.licensePlateNumber + "</td><td>" + t.sum + "</td></tr>";

        console.log(t.sum)
    });
}
//display noncrud
function displaymore() {
    document.getElementById('morearea').innerHTML = "";

    paid.forEach(t => {
        document.getElementById('morearea').innerHTML +=
            "<tr><td>" + t.licensePlateNumber + "</td><td>" + t.name + "</td><td>" + t.parkingFee + "</td></tr>";

        console.log(t.licensePlateNumber)
    });
}
//display noncrud
function displayover() {
    document.getElementById('overarea').innerHTML = "";

    over.forEach(t => {
        document.getElementById('overarea').innerHTML +=
            "<tr><td>" + t.licensePlateNum + "</td><td>" + t.personId + "</td><td>" + t.name + "</td><td>" + t.time + "</td><td>" + t.fee + "</td></tr>";

        console.log(t.time)
    });
}