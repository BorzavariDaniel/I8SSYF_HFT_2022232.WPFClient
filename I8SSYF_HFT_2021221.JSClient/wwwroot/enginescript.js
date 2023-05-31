let engine = [];
let connection = null;
getdata();
setupSignalR();
let engineIdToUpdate = -1;


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:64139/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("EngineCreated", (user, message) => {
        getdata();
        console.log(user);
        console.log(message);
    });

    connection.on("EngineDeleted", (user, message) => {
        getdata();
    });

    connection.on("EngineUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};


async function getdata() {
    await fetch('http://localhost:64139/Engine')
        .then(x => x.json())
        .then(y => {
            engine = y;
            console.log(engine);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    engine.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.fuel + "</td><td>"
            + t.numOfCylinders + "</td><td>" +
            + t.engineId + "</td><td>" +
            `<button type="button" onclick="remove(${t.engineId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.engineId})">Update</button>`
            + "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:64139/Engine/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let jsfuel = document.getElementById('enginefuel').value;
    let jsnumOfCylinders = parseInt(document.getElementById('enginenumOfCylinder').value);

    fetch('http://localhost:64139/Engine/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { fuel: jsfuel, numOfCylinders: jsnumOfCylinders }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function update() {
    let jsfuel = document.getElementById('enginefueltoupdate').value;
    let jsnumOfCylinders = parseInt(document.getElementById('numOfCylinderstoupdate').value);

    fetch('http://localhost:64139/Engine/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { fuel: jsfuel, numOfCylinders: jsnumOfCylinders, engineId: engineIdToUpdate }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function showupdate(id) {
    document.getElementById('updateformdiv').style.display = 'flex';
    document.getElementById('enginefueltoupdate').value = engine.find(x => x['engineId'] == id)['fuel'];
    document.getElementById('numOfCylinderstoupdate').value = parseInt(engine.find(x => x['engineId'] == id)['numOfCylinders']);
    engineIdToUpdate = id;
}