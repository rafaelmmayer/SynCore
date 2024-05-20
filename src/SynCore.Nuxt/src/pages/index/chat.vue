<script setup lang="ts">
import type {ChatMessage, SecondaryUser} from "~/types";
import {watch} from "vue";
import {v4} from "uuid";

interface ChatUser extends SecondaryUser {
  isSelected: boolean
}

const apiClient = useApiClient()
const authUser = useAuthUser()

const users = ref<ChatUser[]>([])
const selectedUser = ref<ChatUser>()
const chatMessages = ref<ChatMessage[]>([])
const chatMessageInput = ref<string>('')

watch(selectedUser, async (newUser: ChatUser) => {
  chatMessages.value = await apiClient.getChatMessages()
  chatMessageInput.value = ''
})

function selectUser(user: ChatUser) {
  users.value.forEach((user) => {
    user.isSelected = false
  })
  user.isSelected = true
  selectedUser.value = user
}

async function sendChatMessage() {
  console.log({ to: selectedUser.value, message: chatMessageInput.value })

  const chatMessage: ChatMessage = {
    id: v4(),
    text: chatMessageInput.value,
    owner: {
      id : authUser.user!.id!,
      name: authUser.user!.name!,
    },
    to: {
      id: selectedUser.value!.id,
      name: selectedUser.value!.name
    }
  }

  await apiClient.addChatMessage(chatMessage)
  chatMessages.value.push(chatMessage)
  chatMessageInput.value = ''
}

onMounted(async () => {
  try {
    const res = await apiClient.getAllUsers()
    users.value = res.map(r => {
      return {
        id: r.id,
        name: r.name,
        isSelected : false
      }
    })
  } catch (e) {
    console.error(e)
  }
})
</script>

<template>
  <div class="flex h-full">
    <div style="width: 300px" class="border-right-1 border-gray-200 h-full bg-white flex flex-column gap-2 p-2">
      <div
          v-for="u in users" :key="u.id"
          class="p-2 cursor-pointer"
          :class="{ 'bg-gray-200': u.isSelected }"
          @click="selectUser(u)"
      >
        {{u.name}}
      </div>
    </div>
    <div class="flex-grow-1 flex flex-column gap-3 p-3" v-if="selectedUser">
      <div class="flex-grow-1 flex flex-column justify-content-end gap-1">
        <div
            v-for="m in chatMessages"
            :key="m.id"
            :class="{
              'ownerChatMessage': m.owner.id !== authUser.user!.id,
              'toChatMessage': m.owner.id === authUser.user!.id,
            }"
            class="p-3 border-round"
            style="min-width: 300px; max-width: 350px"
        >
          {{m.text}}
        </div>
      </div>
      <form class="flex align-items-center gap-2" @submit.prevent="sendChatMessage">
        <div class="flex-grow-1">
          <InputText class="w-full" type="text" v-model="chatMessageInput" />
        </div>
        <div>
          <Button
              icon="pi pi-send"
              text
              rounded
              size="small"
              :pt="{ root: 'custom-small-button' }"
              type="submit"
          />
        </div>
      </form>
    </div>
  </div>
</template>

<style scoped>
.toChatMessage {
  color: white;
  background-color: #059669;
  align-self: baseline;
}
.ownerChatMessage {
  align-self: end;
  background-color: #fff;
}
</style>