let ContactsArray = [];
let PhonesArray = [];
let Name = window.document.getElementById('Nametxt');
let Address = window.document.getElementById('Address');
let Email = window.document.getElementById('Email');

function TypeCheck(){
    let typephone = window.document.getElementsByTagName('option');

    for(let positiontype = 0; positiontype < 3; positiontype++)
    {
        if(typephone[positiontype].selected)
        {
            return positiontype;
        }
    }
}
let Position = 0;
function InsertPhone(){
    let NumberPhone = window.document.getElementById('NumberPhone');
    let ShowPhones = window.document.getElementById('ShowPhones');
    let TypePhone = window.document.getElementsByTagName('option');
    let type = TypeCheck();

    ShowPhones.innerHTML +=`<li id="${Position}"> <div id="Marker">${TypePhone[type].innerHTML}</div> ${NumberPhone.value} <span onclick="DeletePhone(${Position})" style="cursor:pointer">❌</span></li>`;

    PhonesArray.push({
        position: Position,
        type: TypePhone[type].value,
        number: NumberPhone.value
    });

    NumberPhone.value = '';
    NumberPhone.focus();

    Position = Position + 1;
    
}
function DeletePhone(Number){
    
    let IdDelete = window.document.getElementById(`${Number}`);

    IdDelete.parentNode.removeChild(IdDelete);
    
    PhonesArray = PhonesArray.filter(ob => ob.position != Number);

}
function Finish(){
    const URL='http://localhost:5000/Contacts';
    const Data={
        name: Name.value,
        address: Address.value,
        email: {
            emailaddress: Email.value
        },
        phones: PhonesArray
    };
    const otherP={
        headers:{
            "content-type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(Data),
        method: "POST"
    };
    fetch(URL, otherP)
}