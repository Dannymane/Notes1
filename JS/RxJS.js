this.activatedRouter.queryParams.subscribe((params) => {
  // this function runs whenever the queryParams observable *emits* a new value
});

this.activatedRouter.queryParams.subscribe((params) => {
	const eanFromNav = params['ean'];
	const disableEan = params['disableEan'] === 'true';
	const backToBasicScanningAfterScan = params['backToBasicScanningAfterScan'] === 'true';
});

// activatedRouter is an instance of Angular’s ActivatedRoute.
// .queryParams is an observable that emits any time the URL query parameters change.

