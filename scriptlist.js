function newcontact(name){
    let newline = window.document.getElementById('newline');
  
    newline.innerHTML += `<tr>
    <td class="name">${name}</td> 
    <td>
    <button type="submit" name="details"> Details <i class="fa-solid fa-file-lines fa-lg"></i></button>
    <button type="submit" name="edit"> Edit <i class="fa-solid fa-pencil fa-lg"></i></button>
    <button type="submit" name="delete"> Delete <i class="fa-solid fa-trash fa-lg"></i></button>
    </td>
    </tr>`;
}

function getAllContacts(){
    const URL='http://localhost:5000/Contacts';
    const opts={
        headers:{
            "Accept": "application/json; charset=UTF-8"
        },
        method: "GET"
    };
    fetch(URL, opts)
    .then((response) => response.json())
    .then((data) => addContactsToTable(data));
}

function addContactsToTable(contactArray){
    console.log(contactArray);

    let newline = window.document.getElementById('newline');
    newline.innerHTML = '';
    newline.innerHTML += `<tr>
    <th class="text">Name Contact</th>
    <th class="text">Options</th>
    </tr>`

    for(let position = 0; position < contactArray.length; position++)
    {
        newcontact(contactArray[position].name);
    }
}
