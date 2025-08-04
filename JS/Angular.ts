//signal - a reactive getter function
const count = signal(0);
// Signals are getter functions - calling them reads their value.
console.log('The count is: ' + count()); //0

//computed
hasInnerEndOfShelf = computed(() =>
  !!this.listItemData().group?.some(item => item.isEndOfShelf)
);

//effect
effect(
  () => {
    const items = this.filteredDataWithSeparators(); // ← reading a signal
    if (items?.length && !this.isLoading() && this.scrollFacade.currentIndex() < 0) {
      this.scrollFacade.setHighlightingStatusToFirst();
      this.scrollFacade.scrollToHighlighted();
    }
  },
  { allowSignalWrites: true }
);


