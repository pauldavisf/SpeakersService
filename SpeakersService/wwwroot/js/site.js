// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var flag = 0;

function stop() {
    $("#submit").preventDefault();
}

$(function() {

  // We can attach the `fileselect` event to all file inputs on the page
  $(document).on('change', ':file', function() {
    var input = $(this),
        numFiles = input.get(0).files ? input.get(0).files.length : 1,
        label = input.val().replace(/\\/g, '/').replace(/.*\//, '');

      var input1 = $(this).parents('.input-group').find(':text');
      var  filename = input[0].files[0].name;

          if (input[0].files[0].name.split('.').pop() !== 'wav')
          {
              flag = 1;
          }
      else if (input[0].files.length > 1)
          {
              flag = 2;
          }
              else
          {
              flag = 0;
          }

      if (flag === 1)
      {
          input1.css("color", "red");
          var log = `Неверный тип файла ${filename}. Требуется WAV!`;
          input1.val(log);
          $("#submit").css("display", "none");
      }
      else if (flag === 2)
      {
          var log = `Выберите один файл`;
              input1.val(log);
              input1.css("color", "red");
          $("#submit").css("display", "none");
      }
          else
      {
          input1.val(filename);
              input1.css("color", "black");
          $("#submit").css("display", "block");
      }

      input.trigger('fileselect', [numFiles, label]);
     
  });
  
});
