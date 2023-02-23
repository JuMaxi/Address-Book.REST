let ContactsArray = [];
let PhonesArray = [];
let Name = window.document.getElementById('Nametxt');
let Address = window.document.getElementById('Address');
let Email = window.document.getElementById('Email');

function TypeCheck(){
    let TypePhone = window.document.getElementsByName('Phone');

    if(TypePhone[0].checked == true)
    {
        return 1;
    }
    else
    {
        if(TypePhone[1].checked == true)
        {
            return 2;
        }
        else
        {
            if(TypePhone[2].checked == true)
            {
                return 3;
            }
        }
    }
}
function InsertIcone(Type){
    if(Type == 1)
    {
        return '📱';
    }
    else
    {
        if(Type == 2)
        {
            return '📞';
        }
        else
        {
            return '☎️';
        }
    }
}

let Position = 0;
function InsertPhone(){
    let NumberPhone = window.document.getElementById('NumberPhone');
    let Type = TypeCheck();
    let ShowPhones = window.document.getElementById('ShowPhones');
    
    
    ShowPhones.innerHTML +=`<li id="${Position}"> ${InsertIcone(Type)} ${NumberPhone.value} <span onclick="DeletePhone(${Position})" style="cursor:pointer">❌</span></li>`;

    PhonesArray.push({
        position: Position,
        type: Type,
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

function Finish(){
    let EndMessage = window.document.getElementById('End');

    ContactsArray.push(`${Name.value}, ${Address.value}, ${Email.value}, ${PhonesArray}`);

    EndMessage.innerHTML = 'Contact successfully added!'
}