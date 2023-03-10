function newcontact(name, id){
    let newline = window.document.getElementById('newline');
  
    newline.innerHTML += `<tr>
    <td class="name">${name}</td> 
    <td>
    <a href="AddressBook.html?ID=${id}&Details=true"><button type="submit" name="details"> Details <i class="fa-solid fa-file-lines fa-lg"></i></button></a>
    <a href="AddressBook.html?ID=${id}"><button type="submit" name="edit"> Edit <i class="fa-solid fa-pencil fa-lg"></i></button></a>
    <button type="submit" name="delete" onclick="deletecontact(${id})"> Delete <i class="fa-solid fa-trash fa-lg"></i></button>
    </td>
    </tr>`;
}

function getAllContacts(){

    let search = window.document.getElementById('search');

    let URL='http://localhost:5000/Contacts'

   if(search.value.length > 0){
       URL = URL + "?filter=" + search.value;
   }
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
        newcontact(contactArray[position].name, contactArray[position].id);
    }
}
function deletecontact(Id){
    let URL='http://localhost:5000/Contacts?NumberID='+Id;

    const opts={
        headers:{
            "Accept": "application/json; charset=UTF-8"
        },
        method: "DELETE"
    };
    fetch(URL, opts)
    .then((response) => getAllContacts());
}
