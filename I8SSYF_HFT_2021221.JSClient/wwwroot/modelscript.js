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
    fetch('http://localhost:64139/Model' + id, {
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
    let shape = document.getElementById('shape').value;
    let id = parseInt(document.getElementById('id').value);
    const newModel = {
        shape: shape,
        id: id,
    }
    fetch('http://localhost:64139/Model', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            newModel, (key, value) => {
                if (key == "shape") {
                    return parseInt(value);
                }
                else {
                    return value
                }
            }
        )

    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function update() {
    let shape = document.getElementById('modeltoupdate').value;
    let id = parseInt(document.getElementById('id').value);
    const updatedModel = {
        id: modelIdToUpdate,
        shape: shape,
        id: id,
    }
    fetch('http://localhost:64139/Model', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            updatedModel, (key, value) => {
                if (key == "shape" || key == "id") {
                    return parseInt(value);
                }
                else {
                    return value
                }
            }
        )

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
    document.getElementById('modeltoupdate').value = model.find(x => x['id'] == id)['shape'];
    document.getElementById('id').value = parseInt(model.find(x => x['id'] == id)['id']);
    modelIdToUpdate = id;
}