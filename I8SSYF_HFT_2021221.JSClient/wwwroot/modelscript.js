let model = [];
let connection = null;
getdata();
setupSignalR();
let modelIdToUpdate = -1;


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:64139/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ModelCreated", (user, message) => {
        getdata();
        console.log(user);
        console.log(message);
    });

    connection.on("ModelDeleted", (user, message) => {
        getdata();
    });

    connection.on("ModelUpdated", (user, message) => {
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
    await fetch('http://localhost:64139/Model')
        .then(x => x.json())
        .then(y => {
            model = y;
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    model.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.shape + "</td><td>"
            + t.modelId + "</td><td>" +
            `<button type="button" onclick="remove(${t.modelId})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.modelId})">Update</button>`
            + "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:64139/Model/' + id, {
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
    let jsshape = document.getElementById('modelshape').value;

    fetch('http://localhost:64139/Model/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { shape: jsshape }),
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function update() {
    let jsshape = document.getElementById('modelshapetoupdate').value;

    fetch('http://localhost:64139/Model/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { shape: jsshape, modelId: modelIdToUpdate }),
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
    document.getElementById('modelshapetoupdate').value = model.find(x => x['modelId'] == id)['shape'];
    modelIdToUpdate = id;
}