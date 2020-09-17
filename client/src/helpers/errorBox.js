export default {
    new (vc, title = 'Error') {
        vc.$root.$bvToast.toast(title, {
            title: 'Chyba',
            variant: 'danger',
            solid: true
          });
    }
}