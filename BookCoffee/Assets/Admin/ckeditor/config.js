/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.fullPage = false;
    config.allowedContent = true;
    config.language = 'vi';
    config.enterMode = CKEDITOR.ENTER_BR;
    config.shiftEnterMode = CKEDITOR.ENTER_P;
    config.filebrowserBrowseUrl = '/~/Assets/Client/Admin/ckfinder/ckfinder.html';
    config.filebrowserImageBrowseUrl = '/~/Assets/Client/Admin/ckfinder/ckfinder.html?Type=Images';
    config.filebrowserFlashBrowseUrl = '/~/Assets/Client/Admin/ckfinder/ckfinder.html?Type=Flash';
    config.filebrowserUploadUrl = '/~/Assets/Client/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Files';
    config.filebrowserImageUploadUrl = '/Data';
    config.filebrowserFlashUploadUrl = '/~/Assets/Client/Admin/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

    CKFinder.setupCKEditor(null, '/~/Assets/Client/Admin/ckfinder/');
};
