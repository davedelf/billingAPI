const form = document.querySelector("#formulario");
const containerCustomers = document.querySelector("#customersTableContainer");
const btnSubmit=document.querySelector("#btnSubmit");
//EVENTS

document.addEventListener("DOMContentLoaded", () => {
  loadCustomers()
});

//Form
form.addEventListener("submit", (e) => {
  e.preventDefault();
  clearHTML()
  loadCustomers()

});


//FUNCTIONS

function loadCustomers(){

  getCustomers()
  .then(customers=>{
    generateCustomersList(customers)
  })
  .catch(error=>{
    console.error("Failed to load",error)
    containerCustomers.innerHTML = `
      <div class="alert alert-danger">Failed to load </div>
    `;
  })
}
//Get Customers
function getCustomers() {
  const url = "https://localhost:7071/api/customers/GetAll";
  return fetch(url).then((response) => {
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return response.json();
  });
}

//Generate Customers List
function generateCustomersList(customers) {
  let table = `
     <table class="table table-bordered">
          <thead>
            <tr>
              <th>Name</th>
              <th>Last Name</th>
              <th>Birthday</th>
              <th>Email</th>
              <th>Document</th>
              <th>Gender</th>
              <th>ImageURL</th>
            </tr>
          </thead>
          <tbody>
    `;

  customers.forEach((customer) => {
    const { name, lastName, birthday, email, document, gender, imageURL } =
      customer;
    table += `
            <tr>
                <td>${name || "N/A"}</td>
                <td>${lastName || "N/A"}</td>
                <td>${birthday || "N/A"}</td>
                <td>${email || "N/A"}</td>
                <td>${document || "N/A"}</td>
                <td>${gender || "N/A"}</td>
                <td>${imageURL || "N/A"}</td>
            
            </tr>
        `;
  });

  table += `
    </tbody>
    </table>
  `;

  document.getElementById("customersTableContainer").innerHTML = table;
}

//Clear HTML
function clearHTML(){
  while(containerCustomers.firstChild){
    containerCustomers.removeChild(containerCustomers.firstChild)
  }

}
