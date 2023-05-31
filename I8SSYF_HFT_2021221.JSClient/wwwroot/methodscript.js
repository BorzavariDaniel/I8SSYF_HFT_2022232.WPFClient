let methoddata = [];
setupSignalR();
AveragePriceByModels();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:64139/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

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

function AveragePriceByModels() {
    document.getElementById('resultdiv').innerHTML = "";
    fetch('http://localhost:64139/AveragePriceByModels')
        .then(x => x.json())
        .then(y => {
            methoddata = y;
            console.log(methoddata);
            const resultContainer = document.getElementById('resultdiv');
            resultContainer.innerHTML = '';

            for (const item of methoddata) {
                const key = item.key;
                const value = item.value;
                const itemElement = document.createElement('div');
                itemElement.textContent = ${ key }: ${ value };
                resultContainer.appendChild(itemElement);
            }
        });
}

function CylindersByDescending() {
    document.getElementById('resultdiv').innerHTML = "";
    fetch('http://localhost:64139/CylindersByDescending')
        .then(x => x.json())
        .then(y => {
            methoddata = y;
            console.log(methoddata);
            const resultContainer = document.getElementById('resultdiv');
            resultContainer.innerHTML = '';

            for (const item of methoddata) {
                const key = item.key;
                const value = item.value;
                const itemElement = document.createElement('div');
                itemElement.textContent = ${ key }: ${ value };
                resultContainer.appendChild(itemElement);
            }
        });
}

function AverageNumberOfCylindersByModels() {
    document.getElementById('resultdiv').innerHTML = "";
    fetch('http://localhost:64139/AverageNumberOfCylindersByModels')
        .then(x => x.json())
        .then(y => {
            methoddata = y;
            console.log(methoddata);
            const resultContainer = document.getElementById('resultdiv');
            resultContainer.innerHTML = '';

            for (const item of methoddata) {
                const key = item.key;
                const value = item.value;
                const itemElement = document.createElement('div');
                itemElement.textContent = ${ key }: ${ value };
                resultContainer.appendChild(itemElement);
            }
        });
}

function SumPriceByModels() {
    document.getElementById('resultdiv').innerHTML = "";
    fetch('http://localhost:64139/SumPriceByModels')
        .then(x => x.json())
        .then(y => {
            methoddata = y;
            console.log(methoddata);
            const resultContainer = document.getElementById('resultdiv');
            resultContainer.innerHTML = '';

            for (const item of methoddata) {
                const key = item.key;
                const value = item.value;
                const itemElement = document.createElement('div');
                itemElement.textContent = ${ key }: ${ value };
                resultContainer.appendChild(itemElement);
            }
        });
}

function CarCountByModels() {
    document.getElementById('resultdiv').innerHTML = "";
    fetch('http://localhost:64139/CarCountByModels')
        .then(x => x.json())
        .then(y => {
            methoddata = y;
            console.log(methoddata);
            const resultContainer = document.getElementById('resultdiv');
            resultContainer.innerHTML = '';

            for (const item of methoddata) {
                const key = item.key;
                const value = item.value;
                const itemElement = document.createElement('div');
                itemElement.textContent = ${ key }: ${ value };
                resultContainer.appendChild(itemElement);
            }
        });
}