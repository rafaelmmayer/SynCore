<script setup lang="ts">

</script>

<template>
  <div class="flex justify-content-center">
    <div style="width: 80%; max-width: 1000px;" class="flex flex-column gap-4">
      <Card class="shadow-none border-1 border-gray-200">
        <template #title>Crie um post</template>
        <template #content>
          <form class="flex flex-column gap-3">
            <Textarea placeholder="Digite algo..." class="bg-gray-100 w-full" ></Textarea>
            <Button class="align-self-end" label="Enviar" icon="pi pi-send"></Button>
          </form>
        </template>
      </Card>

      <Card class="shadow-none border-1 border-gray-200">
        <template #title>
          <div>
            <span class="pi pi-user"></span>
            <span style="font-size: 16px" class="ml-2">Rafael Mayer</span>
          </div>
        </template>
        <template #content>
          <div class="flex flex-column gap-3">
            <div style="max-height: 300px" class="w-full bg-gray-100 p-3 border-round overflow-auto">
              Lorem ipsum dolor sit amet consectetur. Scelerisque adipiscing vitae sodales arcu habitant amet sed. Et pellentesque nunc eget bibendum placerat ut lorem tortor faucibus. Orci interdum vitae viverra sit. Senectus sed mattis enim amet. Quam fames velit proin accumsan. Mauris nec dolor eros viverra. Amet nunc varius nullam non commodo sit. Augue massa pulvinar aliquam cursus eu sed congue. Tortor faucibus ornare aliquet adipiscing lacus nisi volutpat semper. Fringilla et vulputate placerat sit pharetra ut augue adipiscing. Tempus dolor sed sed cras at. Justo viverra mauris condimentum metus lorem ac.
              Lorem ipsum dolor sit amet consectetur. Scelerisque adipiscing vitae sodales arcu habitant amet sed. Et pellentesque nunc eget bibendum placerat ut lorem tortor faucibus. Orci interdum vitae viverra sit. Senectus sed mattis enim amet. Quam fames velit proin accumsan. Mauris nec dolor eros viverra. Amet nunc varius nullam non commodo sit. Augue massa pulvinar aliquam cursus eu sed congue. Tortor faucibus ornare aliquet adipiscing lacus nisi volutpat semper. Fringilla et vulputate placerat sit pharetra ut augue adipiscing. Tempus dolor sed sed cras at. Justo viverra mauris condimentum metus lorem ac.
              Lorem ipsum dolor sit amet consectetur. Scelerisque adipiscing vitae sodales arcu habitant amet sed. Et pellentesque nunc eget bibendum placerat ut lorem tortor faucibus. Orci interdum vitae viverra sit. Senectus sed mattis enim amet. Quam fames velit proin accumsan. Mauris nec dolor eros viverra. Amet nunc varius nullam non commodo sit. Augue massa pulvinar aliquam cursus eu sed congue. Tortor faucibus ornare aliquet adipiscing lacus nisi volutpat semper. Fringilla et vulputate placerat sit pharetra ut augue adipiscing. Tempus dolor sed sed cras at. Justo viverra mauris condimentum metus lorem ac.
            </div>
            <div>
              <Button icon="pi pi-heart" text label="10 likes" severity="danger" :pt="{ label: 'custom-label-like-btn' }" />
              <Button icon="pi pi-bars" text label="24 comentários" severity="info" />
            </div>
          </div>
        </template>
      </Card>
    </div>
  </div>
</template>

<style>
.custom-label-like-btn {
  color: #64748b;
}
</style>