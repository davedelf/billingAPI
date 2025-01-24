const form = document.querySelector("#form");
const containerCustomers = document.querySelector("#customersTableContainer");

//Inputs
const name = document.querySelector("#name");
const lastName = document.querySelector("#lastName");
const documentField = document.querySelector("#document");
const email = document.querySelector("#email");
const gender = document.querySelector("#gender");
const bornDate = document.querySelector("#bornDate");
// EVENTS
document.addEventListener("DOMContentLoaded", () => {
  loadCustomers();
  validateForm();
});

// FUNCTIONS


//Format Date
function formatDate(dateString) {
  if (!dateString) return "N/A";
  let date = new Date(dateString);
  if (isNaN(date)) return "N/A";
  // return date.toISOString().split("T")[0];
  return isNaN(date) ? "N/A" : date.toISOString().split("T")[0];
}

// Fetch customers from the server
function getCustomers() {
  const url = "https://localhost:7071/api/customers/GetAll";
  return fetch(url)
    .then((res) => {
      if (!res.ok) {
        throw new Error(`HTTP Error: ${res.status}`);
      }
      return res.json();
    })
    .catch((err) => {
      console.log("Failed to fetch customers", err);
      return [];
    });
}

//Handler submit
function handleFormSubmit(event) {
  if (event) event.preventDefault();
  const customer = createCustomerObject();
  postCustomer(customer);
  form.reset();
}

//Post customer
function postCustomer(customer) {
  const url = "https://localhost:7071/api/customers/Post";

  fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(customer),
  })
    .then((res) => {
      if (!res.ok) {
        return res.text().then((errMsg) => {
          throw new Error(errMsg);
        });
      }

      const contentType = res.headers.get("content-type");
      if (contentType && contentType.includes("application/json")) {
        return res.json();
      } else {
        return res.text().then((text) => {
          throw new Error(`Respuesta inesperada del servidor: ${text}`);
        });
      }
    })
    .then((dat) => {
      console.log("Customer created successfully", dat);

      const divSuccess = document.createElement("DIV");
      const successImg = document.createElement("IMG");
      successImg.src = "https://http.cat/images/200.jpg";
      divSuccess.appendChild(successImg);

      containerCustomers.appendChild(divSuccess);

      form.reset();
      loadCustomers();

      setTimeout(() => {
        divSuccess.remove();
      }, 5000);
    })

    .catch((err) => {
      const divError = document.createElement("DIV");
      const error = document.createElement("P");
      const errorImg = document.createElement("IMG");

      errorImg.src = "https://http.cat/images/400.jpg";
      error.textContent = err.message;
      error.classList.add("text-red-500", "text-center", "font-serif");
      divError.appendChild(errorImg);
      divError.appendChild(error);

      containerCustomers.appendChild(divError);

      setTimeout(() => {
        divError.remove();
      }, 5000);
    });
}

//Delete customer
function deleteCustomer(docum) {
  const url = `https://localhost:7071/api/customers/DeleteByDocument?document=${docum}`;

  fetch(url, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    }
  })
    .then((res) => {
      if (!res.ok) {
        return res.text().then((errMsg) => {
          throw new Error(`Error al eliminar: ${res.status} - ${errMsg}`);
        });
      }
      return res.text();
    })
    .then((dat) => {
      console.log("Cliente eliminado correctamente:", dat);
      loadCustomers(); // Recargar la lista de clientes después de eliminar
    })
    .catch((err) => {
      console.error("Error eliminando cliente:", err);
    });
}

// Create Customer Object
function createCustomerObject() {
  return {
    name: name.value.trim(),
    lastName: lastName.value.trim(),
    document: parseInt(documentField.value.trim()),
    email: email.value.trim(),
    bornDate: bornDate.value,
    gender: parseInt(gender.value) || 0,
  };
}
// Generate customer table
function generateCustomersTable(customers) {
  if (!customers.length) {
    containerCustomers.innerHTML =
      '<div class="alert alert-warning">No customers found</div>';
    return;
  }

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

  customers.forEach(({ name, lastName, bornDate, email, document, gender }) => {
   
    table += `
            <tr>
                <td>${name || "N/A"}</td>
                <td>${lastName || "N/A"}</td>
                <td>${formatDate(bornDate)}</td>
                <td>${email || "N/A"}</td>
                <td>${document || "N/A"}</td>
                <td>${gender === 0 ? "Male" : "Female"}</td>
                <td>
                  <button class="btn-update" data-document=${document}>Update</button>
                </td>
                <td>
                  <button class="btn-delete" data-document=${document}>Delete</button>
                </td>

            </tr>
        `;
  });

  table += `
    </tbody>
    </table>
  `;

  containerCustomers.innerHTML = table;


  //Button Events
  document.querySelectorAll(".btn-delete").forEach(btn=>{
    btn.addEventListener("click",e=>{
      const documentId=Number(e.target.getAttribute("data-document"))
      deleteCustomer(documentId)
      loadCustomers()
    })
  })
}

// Clear HTML content
function clearHTML() {
  // containerCustomers.innerHTML = "";
  // while (containerCustomers.firstChild) {
  //   containerCustomers.removeChild(containerCustomers.firstChild);
  // }
  containerCustomers.innerHTML = "";
}

//Load customersList
function loadCustomers() {
  clearHTML();

  getCustomers()
    .then((customers) => {
      generateCustomersTable(customers);
    })
    .catch((error) => {
      console.error("Failed to load customers:", error);
      containerCustomers.innerHTML = `
        <div class="alert alert-danger">Failed to load customers</div>
      `;
    });
}

// Validate form
function validateForm() {
  $("#form").validate({
    rules: {
      name: {
        required: true,
        minlength: 3,
      },
      lastName: {
        required: true,
        minlength: 3,
      },
      document: {
        required: true,
        digits: true,
      },
      email: {
        required: true,
        email: true,
      },
      bornDate: {
        required: true,
      },
      gender: {
        required: true,
      },
    },
    messages: {
      name: {
        required: "Please enter a name",
        minlength: "Name must be at least 3 characters long",
      },
      lastName: {
        required: "Please enter a last name",
        minlength: "Last name must be at least 3 characters long",
      },
      document: {
        required: "Please enter a document number",
        digits: "Please enter a valid document number",
      },
      email: {
        required: "Please enter an email",
        email: "Please enter a valid email",
      },
      bornDate: {
        required: "Please select a born date",
      },
      gender: {
        required: "Plase select a gender",
      },
    },
    submitHandler: function () {
      handleFormSubmit();
    },
  });
}



