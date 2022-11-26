<template>
  <div>
    <h4>Введите строку запроса</h4>
    <InputText type="text" v-model="input" class="input"/>
  </div>

  <h4>Искать по:</h4>
  <div class="selector-container">
    <div>
      <RadioButton name="author" input-id="author" value="author" v-model="type"/>
      <label for="author">Автору</label>
    </div>
    <div>
      <RadioButton name="category" input-id="category" value="category" v-model="type"/>
      <label for="category">Категории</label>
    </div>
    <div>
      <RadioButton name="title" input-id="title" value="title" v-model="type"/>
      <label for="title">Названию</label>
    </div>
  </div>

  <div>
    <button @click="request(input, type)">Искать</button>
  </div>

  <div class="table-container" v-if="list?.length > 0">
    <DataTable :value="list" responsiveLayout="scroll">
        <Column field="Title" header="Title"></Column>
        <Column field="Category" header="Category"></Column>
        <Column field="PublicationDate" header="Date"></Column>
        <Column field="Pages" header="Pages"></Column>
    </DataTable>
  </div>
</template>

<script>
import Soap from '@/core/services/soap.js'

export default {
  name: 'LibraryComponent',
  data() {
    return {
      list: [],
      input: '',
      type: ''
    }
  },
  methods: {
    request: async function(text, type) {
      let soap = new Soap;
      switch (type) {
        case 'author': this.list = await soap.GetByAuthor(text); break;
        case 'category': this.list = await soap.GetByCategory(text); break;
        case 'title': this.list = await soap.GetByTitle(text); break;
      }
    }
  }
}
</script>

<style scoped lang="scss">
button {
  height: 30px;
  width: 80px;
  margin-top: 10px;
}

.input {
  height: 25px;
  width: 150px;
  font-size: 16px;
}

.selector-container {
  display: flex;
  justify-content: center;

  div {
    margin: 0px 10px;
    display: flex;
  }
  
  label {
    text-align: right;
  }
}

.table-container {
  margin-top: 20px;
  display: flex;
  justify-content: center;
}

::v-deep(.p-datatable) {
  width: 70vw;
}

::v-deep(.p-column-header-content) {
  justify-content: start;
  border-bottom: 1px solid black;
}

::v-deep(tr) {
  text-align: start;
}
</style>
