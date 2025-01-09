const form = document.querySelector("#formulario");
const containerCustomers = document.querySelector("#customersTableContainer");
const inputName = document.querySelector("#inputName");
const inputLastName = document.querySelector("#inputLastName");
const inputDocument = document.querySelector("#inputDocument");
const inputEmail = document.querySelector("#inputEmail");
const dtpBornDate = document.querySelector("#dtpBornDate");
const radioMale = document.querySelector("#radioMale");
const radioFemale = document.querySelector("#radioFemale");

// EVENTS
document.addEventListener("DOMContentLoaded", () => {
  loadCustomers();
});

form.addEventListener("submit", (e) => {
  e.preventDefault();

  if (validateForm()) {
    const newCustomer = createCustomerObject();
    console.log("New Customer:", newCustomer);
    postCustomer(newCustomer); // Llama a la función para enviar al servidor
  }
});

// FUNCTIONS

// Load customers from the server
function loadCustomers() {
  clearHTML();

  getCustomers()
    .then((customers) => {
      generateCustomersList(customers);
    })
    .catch((error) => {
      console.error("Failed to load customers:", error);
      containerCustomers.innerHTML = `
        <div class="alert alert-danger">Failed to load customers</div>
      `;
    });
}

// Fetch customers from the server
function getCustomers() {
  const url = "https://localhost:7071/api/customers/GetAll";
  return fetch(url).then((response) => {
    if (!response.ok) {
      throw new Error(`HTTP error! Status: ${response.status}`);
    }
    return response.json();
  });
}

// Generate customer table
function generateCustomersList(customers) {
  let table = `
     <table class="table table-bordered">
          <thead>
            <tr>
              <th>Name</th>
              <th>Last Name</th>
              <th>Born Date</th>
              <th>Email</th>
              <th>Document</th>
              <th>Gender</th>
            </tr>
          </thead>
          <tbody>
    `;

  customers.forEach((customer) => {
    const { name, lastName, bornDate, email, document, gender } = customer;
    table += `
            <tr>
                <td>${name || "N/A"}</td>
                <td>${lastName || "N/A"}</td>
                <td>${bornDate || "N/A"}</td>
                <td>${email || "N/A"}</td>
                <td>${document || "N/A"}</td>
                <td>${gender === 0 ? "Male" : "Female"}</td>
            </tr>
        `;
  });

  table += `
    </tbody>
    </table>
  `;

  containerCustomers.innerHTML = table;
}

// Clear HTML content
function clearHTML() {
  containerCustomers.innerHTML = "";
}

// Validate form inputs
function validateForm() {
  const name = inputName.value.trim();
  const lastName = inputLastName.value.trim();
  const email = inputEmail.value.trim();
  const document = inputDocument.value.trim();
  const bornDate = dtpBornDate.value;
  const isMale = radioMale.checked;
  const isFemale = radioFemale.checked;

  if (!name || !lastName || !email || !document || !bornDate) {
    alert("All fields are required.");
    return false;
  }

  if (!isMale && !isFemale) {
    alert("Please select a gender.");
    return false;
  }

  return true;
}

// Create Customer Object
function createCustomerObject() {
  const name = inputName.value.trim();
  const lastName = inputLastName.value.trim();
  const email = inputEmail.value.trim();
  const document = inputDocument.value.trim();
  const bornDate = dtpBornDate.value.trim();
  const gender = radioMale.checked ? 0 : 1;

  return newCustomer={ name, lastName, bornDate, email, document, gender };
}

// Post customer to the server
function postCustomer(customer) {
  const url = "https://localhost:7071/api/customers";
  fetch(url, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(customer),
  })
    .then((response) => {
      if (!response.ok) {
        throw new Error(`HTTP error! Status: ${response.status}`);
      }
      return response.json();
    })
    .then((data) => {
      alert("Customer added successfully!");
      loadCustomers(); // Reload customers after adding
    })
    .catch((error) => {
      console.error("Failed to add customer:", error);
      alert("Failed to add customer.");
    });
}
