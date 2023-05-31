let methoddata = [];
setupSignalR();
AveragePriceByModels();
CylindersByDescending();
AverageNumberOfCylindersByModels();
SumPriceByModels();
CarCountByModels();

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
    fetch('http://localhost:64139/Method/AveragePriceByModels')
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
                itemElement.textContent = `${key}: ${value }`;
                resultContainer.appendChild(itemElement);
            }
        });
}

function CylindersByDescending() {
    document.getElementById('resultdiv2').innerHTML = "";
    fetch('http://localhost:64139/Method/CylindersByDescending')
        .then(x => x.json())
        .then(y => {
            methoddata = y;
            console.log(methoddata);
            const resultContainer = document.getElementById('resultdiv2');
            resultContainer.innerHTML = '';

            for (const item of methoddata) {
                const key = item.key;
                const value = item.value;
                const itemElement = document.createElement('div');
                itemElement.textContent = `${key}: ${value}`;
                resultContainer.appendChild(itemElement);
            }
        });
}

function AverageNumberOfCylindersByModels() {
    document.getElementById('resultdiv3').innerHTML = "";
    fetch('http://localhost:64139/Method/AverageNumberOfCylindersByModels')
        .then(x => x.json())
        .then(y => {
            methoddata = y;
            console.log(methoddata);
            const resultContainer = document.getElementById('resultdiv3');
            resultContainer.innerHTML = '';

            for (const item of methoddata) {
                const key = item.key;
                const value = item.value;
                const itemElement = document.createElement('div');
                itemElement.textContent = `${key}: ${value}`;
                resultContainer.appendChild(itemElement);
            }
        });
}

function SumPriceByModels() {
    document.getElementById('resultdiv4').innerHTML = "";
    fetch('http://localhost:64139/Method/SumPriceByModels')
        .then(x => x.json())
        .then(y => {
            methoddata = y;
            console.log(methoddata);
            const resultContainer = document.getElementById('resultdiv4');
            resultContainer.innerHTML = '';

            for (const item of methoddata) {
                const key = item.key;
                const value = item.value;
                const itemElement = document.createElement('div');
                itemElement.textContent = `${key}: ${value}`;
                resultContainer.appendChild(itemElement);
            }
        });
}

function CarCountByModels() {
    document.getElementById('resultdiv5').innerHTML = "";
    fetch('http://localhost:64139/Method/CarCountByModels')
        .then(x => x.json())
        .then(y => {
            methoddata = y;
            console.log(methoddata);
            const resultContainer = document.getElementById('resultdiv5');
            resultContainer.innerHTML = '';

            for (const item of methoddata) {
                const key = item.key;
                const value = item.value;
                const itemElement = document.createElement('div');
                itemElement.textContent = `${key}: ${value}`;
                resultContainer.appendChild(itemElement);
            }
        });
}