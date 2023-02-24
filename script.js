let ContactsArray = [];
let PhonesArray = [];
let Name = window.document.getElementById('Nametxt');
let Address = window.document.getElementById('Address');
let Email = window.document.getElementById('Email');

function TypeCheck(){
    let TypePhone = window.document.getElementsByTagName('select');
    if(TypePhone[0].value == '📱')
    {
        return 1;
    }
    else
    {
        if(TypePhone[0].value =='📞')
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }
}
let Position = 0;
function InsertPhone(){
    let NumberPhone = window.document.getElementById('NumberPhone');
    let ShowPhones = window.document.getElementById('ShowPhones');
    let TypePhone = window.document.getElementsByTagName('select');
    
    ShowPhones.innerHTML +=`<li id="${Position}"> ${TypePhone[0].value} ${NumberPhone.value} <span onclick="DeletePhone(${Position})" style="cursor:pointer">❌</span></li>`;

    PhonesArray.push({
        position: Position,
        type: TypeCheck(),
        phone: NumberPhone.value
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

function CheckPhones(arrayphones){
    for(let positionarray = 0; positionarray < arrayphones.length; arrayphones++)
    {
        return arrayphones[positionarray];
    }
}
    

function Finish(){
    window.alert(`Oi`);
    let EndMessage = window.document.getElementById('End');

    
}