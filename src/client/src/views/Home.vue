<template>
  <div>
    <PageHeader title="Vítejte u Studentby"></PageHeader>
    <b-card-group deck>
      <b-card
        title="Student"
        :img-src="require('../assets/student.jpg')"
        img-alt="Student"
        img-top
      >
        <b-card-text>
          Pro studenty nabízíme jednoduché a pohodlné vyhledávání jednorázových
          brigád na termín, kterým jim bude vyhovovat. Stačí vybrat nabídku a
          další den můžete jít!
        </b-card-text>
      </b-card>

      <b-card
        title="Zaměstnavatel"
        :img-src="require('../assets/employer.jpg')"
        img-alt="Zaměstnavatel"
        img-top
        >
        <b-card-text>
          Pro zaměstnavatele odpadá starost s výběrem vhodných zájemců, jejich
          koordinací a vyplácením odměn. Stačí vytvořit nabídku a my už se o vše
          postaráme.
        </b-card-text>
      </b-card>
    </b-card-group>
  </div>
</template>

<script>
import PageHeader from "../components/PageHeader";

export default {
  name: "Home",
  components: {
    PageHeader,
  },
};
</script>
