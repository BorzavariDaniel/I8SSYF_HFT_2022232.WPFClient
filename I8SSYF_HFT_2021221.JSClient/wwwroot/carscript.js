let cars = [];
let connection = null;
getdata();
setupSignalR();
let carIdToUpdate = -1;


function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:64139/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("CarCreated", (user, message) => {
        getdata();
        console.log(user);
        console.log(message);
    });

    connection.on("CarDeleted", (user, message) => {
        getdata();
    });

    connection.on("CarUpdated", (user, message) => {
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
    await fetch('http://localhost:64139/Car')
        .then(x => x.json())
        .then(y => {
            cars = y;
            console.log(cars);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    cars.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.name + "</td><td>"
            + t.price + "</td><td>" +
            + t.id + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:64139/Car/' + id, {
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
    let name = document.getElementById('name').value;
    let price = parseInt(document.getElementById('price').value);
    const newCar = {
        name: name,
        price: price,
    }
    fetch('http://localhost:64139/Car/', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            newCar, (key, value) => {
                if (key == "price") {
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
    let name = document.getElementById('cartoupdate').value;
    let price = parseInt(document.getElementById('price').value);
    const updatedCar = {
        id: carIdToUpdate,
        name: name,
        price: price
    }
    fetch('http://localhost:64139/Car/', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            updatedCar, (key, value) => {
                if (key == "price" || key == "id") {
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
    document.getElementById('cartoupdate').value = cars.find(x => x['id'] == id)['name'];
    document.getElementById('price').value = parseInt(cars.find(x => x['id'] == id)['price']);
    carIdToUpdate = id;
}