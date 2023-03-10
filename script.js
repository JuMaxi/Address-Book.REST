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
let TypePhone = window.document.getElementsByTagName('option');
let NumberPhone = window.document.getElementById('NumberPhone');
function InsertPhone(id){
    let ShowPhones = window.document.getElementById('ShowPhones');
    
    let type = '';
    if(id == null){
        type = TypeCheck();
    }
    else{
        type = Number(TypePhone.value) - 1;
    }


    let span = '<span onclick="DeletePhone(${Position})" style="cursor:pointer">❌</span>';

    if(Details != null){
        span = '';
    }

    ShowPhones.innerHTML +=`<li id="${Position}"> <div id="Marker">${TypePhone[type].innerHTML}</div> ${NumberPhone.value} ${span}</li>`;

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

    let endpoint = 'POST';
    let ident = 0;

    if(ID != null){
        endpoint = 'PUT';
        ident = ID;
    }

    const URL='http://localhost:5000/Contacts';
    const Data={
        id: ident,
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
       
        method: endpoint
    };
    fetch(URL, otherP)
}

let urlParams = new URLSearchParams(window.location.search);
let ID = urlParams.get('ID');
let Details = urlParams.get('Details');

if(ID != null){
    let URL='http://localhost:5000/Contacts/'+ID;

    const opts={
        headers:{
            "Accept": "application/json; charset=UTF-8"
        },
        method: "Get"
    };
    fetch(URL, opts)
    .then((response) => response.json())
    .then((data) => writedata(data));
}
if(Details != null){
    window.document.getElementById('Nametxt').disabled = true;
    window.document.getElementById('Address').disabled = true;
    window.document.getElementById('Email').disabled = true;
    window.document.getElementById('NumberPhone').style.display = 'none';

    let select = window.document.getElementsByTagName('select');
    for(let position = 0; position < select.length; position++){
        select[position].style.display = 'none';
    }
    window.document.getElementById('add').style.display = 'none';
    window.document.getElementById('finish').style.display = 'none';

    window.document.getElementById('phones').innerHTML = 'Phones:';
}

function writedata(response){
    Name.value = response.name;
    Address.value = response.address;
    Email.value = response.email.emailAddress;

    for(let Position = 0; Position < response.phones.length; Position++){
            
        NumberPhone.value = response.phones[Position].number;
        TypePhone.value = response.phones[Position].type;

        InsertPhone(response.id);
    }
}





   
    



